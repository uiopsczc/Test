using System;
using System.Collections.Generic;
using System.Threading;

namespace CsCat
{
	/// <summary>
	///   双缓冲 来自于《游戏编程模式》->双缓冲  但对其有深入了解
	///   ManualResetEvent:
	///   除非手工调用了ManualResetEvent.Reset()方法
	///   则ManualResetEvent将一直保持有信号状态
	///   ManualResetEvent也就可以同时唤醒多个线程继续执行
	///   AutoResetEvent:
	///   当某个线程得到信号后
	///   AutoResetEvent会自动又将信号置为不发送状态
	///   则其他调用WaitOne的线程只有继续等待
	///   也就是说AutoResetEvent一次只唤醒一个线程
	/// </summary>
	public class DoubleBuffer<T>
	{
		/// <summary>
		///   Conusme中需要对缓存的数据进行的处理
		/// </summary>
		public Action<T> consumeHandle;

		/// <summary>
		///   当前队列
		/// </summary>
		private volatile Queue<T> _currentQueue;

		/// <summary>
		///   队列中是否有新的数据缓存了，需要处理的锁,当WaitOne的时候阻塞，直到其他地方有Set()调用才有机会获得锁，自己线程得以执行
		/// </summary>
		private readonly AutoResetEvent _dataAvailableEvent = new AutoResetEvent(false);

		/// <summary>
		///   Produce中Enqueue的时候需要进行的处理
		/// </summary>
		public Action<T> produceHandle;

		private readonly Queue<T> _queue0 = new Queue<T>();
		private readonly Queue<T> _queue1 = new Queue<T>();

		/// <summary>
		///   阻塞线程的锁，当WaitOne的时候阻塞，直到其他地方有Set()调用才有机会获得锁，自己线程得以执行
		/// </summary>
		private readonly AutoResetEvent _unblockEvent = new AutoResetEvent(true);

		public DoubleBuffer(Action<T> consumeHandle, Action<T> produceHandle = null)
		{
			_currentQueue = _queue0;
			this.consumeHandle = consumeHandle;
			this.produceHandle = produceHandle;
		}

		public void Produce(T data)
		{
			//1.等待其他线程的Produce的unblockEvent的Set()释放获得锁,从而才有机会获得锁，自己线程得以执行
			//2.等待Consume()线程中的unblockEvent的unblockEvent的Set()释放获得锁,从而才有机会获得锁，自己线程得以执行
			_unblockEvent.WaitOne();

			produceHandle?.Invoke(data);
			//写数据 
			_currentQueue.Enqueue(data);


			//告诉Consume()有数据，可以供handle处理
			//这里有操作的空间，譬如可以改成在queue数量达到多少时才告诉Consume执行
			_dataAvailableEvent.Set();

			//释放unblockEvent锁
			_unblockEvent.Set();
		}

		/// <summary>
		///   只能存在一个consume的线程，但consume的处理速度明显要比Produce的速度快，所以才需要双缓冲来进行处理
		///   如果存在多个consume线程会出错
		/// </summary>
		public void Consume()
		{
			//等待Produce中dataAvailableEvent的Set()来通知获得锁，得知有数据需要处理
			_dataAvailableEvent.WaitOne();

			//swap queues
			_unblockEvent.WaitOne(); //等待当前的Produce线程Enqueue操作完成
			var readQueue = _currentQueue; //当前需要处理的数据
			_currentQueue =
			  _currentQueue == _queue0 ? _queue1 : _queue0; // 交换两个Queue,旧的队列在readQueue中进行处理，currentQueue变为另一个的队列供Enqueue
			_unblockEvent.Set(); // 释放unblockEvent的锁，其他的Produce()可以获取unblockEvent的锁进行Enqueue操作


			//数据处理   这里需要时间处理数据，但同时缓冲区是开放给其他Produce（）进行Enqueue的
			//如果多个Consume线程，会在这里出错，因为这时候可能swap queue 导致readQueue指向了另一队列
			while (readQueue.Count > 0)
			{
				var data = readQueue.Dequeue();
				consumeHandle?.Invoke(data);
			}
		}
	}
}