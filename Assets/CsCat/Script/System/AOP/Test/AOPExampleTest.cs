using UnityEngine;

namespace CsCat
{
	public class AOPExampleTest : MonoBehaviour
	{
		private void Start()
		{
			var e1 = new AOPExample1();
			e1.name = "example";
			e1.CallHello("xaiofeiyu", new Circle(Vector2.zero, 5));
			e1.CallHello("小飞鱼", new Circle(Vector2.zero, 10));
			e1.CallWorld("大肥鱼");


			//AOPExample1 e11 = new AOPExample1();
			//e11.name = "世界杯";
			//e11.CallHello("中国队");


			//AOPExample2 e2 = new AOPExample2();
			//e2.name = "群众";
			//e2.CallHello("在吃西瓜");
		}
	}
}