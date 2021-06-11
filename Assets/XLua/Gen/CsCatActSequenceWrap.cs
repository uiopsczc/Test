#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class CsCatActSequenceWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CsCat.ActSequence);
			Utils.BeginObjectRegister(type, L, translator, 0, 16, 3, 2);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Start", _m_Start);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Update", _m_Update);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Next", _m_Next);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Break", _m_Break);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Clear", _m_Clear);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Append", _m_Append);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InsertAt", _m_InsertAt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AppendActStart", _m_AppendActStart);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InsertAtActStart", _m_InsertAtActStart);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Coroutine", _m_Coroutine);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WaitForSeconds", _m_WaitForSeconds);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WaitForFrames", _m_WaitForFrames);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WaitForEndOfFrame", _m_WaitForEndOfFrame);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WaitUntil", _m_WaitUntil);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnDespawn", _m_OnDespawn);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "cur_act", _g_get_cur_act);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "act_list", _g_get_act_list);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "cur_act_index", _g_get_cur_act_index);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "act_list", _s_set_act_list);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "cur_act_index", _s_set_cur_act_index);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					CsCat.ActSequence gen_ret = new CsCat.ActSequence();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<CsCat.ActSequence>(L, 2))
				{
					CsCat.ActSequence _sequence = (CsCat.ActSequence)translator.GetObject(L, 2, typeof(CsCat.ActSequence));
					
					CsCat.ActSequence gen_ret = new CsCat.ActSequence(_sequence);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<UnityEngine.MonoBehaviour>(L, 2))
				{
					UnityEngine.MonoBehaviour _owner = (UnityEngine.MonoBehaviour)translator.GetObject(L, 2, typeof(UnityEngine.MonoBehaviour));
					
					CsCat.ActSequence gen_ret = new CsCat.ActSequence(_owner);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 3 && translator.Assignable<UnityEngine.MonoBehaviour>(L, 2) && translator.Assignable<CsCat.ActSequence>(L, 3))
				{
					UnityEngine.MonoBehaviour _owner = (UnityEngine.MonoBehaviour)translator.GetObject(L, 2, typeof(UnityEngine.MonoBehaviour));
					CsCat.ActSequence _parent = (CsCat.ActSequence)translator.GetObject(L, 3, typeof(CsCat.ActSequence));
					
					CsCat.ActSequence gen_ret = new CsCat.ActSequence(_owner, _parent);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 4 && translator.Assignable<UnityEngine.MonoBehaviour>(L, 2) && translator.Assignable<CsCat.ActSequence>(L, 3) && (LuaAPI.lua_isnil(L, 4) || LuaAPI.lua_type(L, 4) == LuaTypes.LUA_TSTRING))
				{
					UnityEngine.MonoBehaviour _owner = (UnityEngine.MonoBehaviour)translator.GetObject(L, 2, typeof(UnityEngine.MonoBehaviour));
					CsCat.ActSequence _parent = (CsCat.ActSequence)translator.GetObject(L, 3, typeof(CsCat.ActSequence));
					string _id = LuaAPI.lua_tostring(L, 4);
					
					CsCat.ActSequence gen_ret = new CsCat.ActSequence(_owner, _parent, _id);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.ActSequence constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.MonoBehaviour _owner = (UnityEngine.MonoBehaviour)translator.GetObject(L, 2, typeof(UnityEngine.MonoBehaviour));
                    CsCat.ActSequence _parent = (CsCat.ActSequence)translator.GetObject(L, 3, typeof(CsCat.ActSequence));
                    string _id = LuaAPI.lua_tostring(L, 4);
                    
                    gen_to_be_invoked.Init( _owner, _parent, _id );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Start(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Start(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Update(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Update(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Next(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Next(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Break(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Break(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Clear(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Clear(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Append(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<CsCat.Act>(L, 2)) 
                {
                    CsCat.Act _act = (CsCat.Act)translator.GetObject(L, 2, typeof(CsCat.Act));
                    
                        CsCat.Act gen_ret = gen_to_be_invoked.Append( _act );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Collections.Generic.List<CsCat.Act>>(L, 2)) 
                {
                    System.Collections.Generic.List<CsCat.Act> _act_list = (System.Collections.Generic.List<CsCat.Act>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<CsCat.Act>));
                    
                        CsCat.Act gen_ret = gen_to_be_invoked.Append( _act_list );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.ActSequence.Append!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InsertAt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<CsCat.Act>(L, 3)) 
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    CsCat.Act _act = (CsCat.Act)translator.GetObject(L, 3, typeof(CsCat.Act));
                    
                        CsCat.Act gen_ret = gen_to_be_invoked.InsertAt( _index, _act );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Collections.Generic.List<CsCat.Act>>(L, 3)) 
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    System.Collections.Generic.List<CsCat.Act> _act_list = (System.Collections.Generic.List<CsCat.Act>)translator.GetObject(L, 3, typeof(System.Collections.Generic.List<CsCat.Act>));
                    
                        CsCat.Act gen_ret = gen_to_be_invoked.InsertAt( _index, _act_list );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.ActSequence.InsertAt!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AppendActStart(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<System.Action<CsCat.Act>>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    System.Action<CsCat.Act> _start_callback = translator.GetDelegate<System.Action<CsCat.Act>>(L, 2);
                    bool _is_exit_at_once = LuaAPI.lua_toboolean(L, 3);
                    
                        CsCat.Act gen_ret = gen_to_be_invoked.AppendActStart( _start_callback, _is_exit_at_once );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Action<CsCat.Act>>(L, 2)) 
                {
                    System.Action<CsCat.Act> _start_callback = translator.GetDelegate<System.Action<CsCat.Act>>(L, 2);
                    
                        CsCat.Act gen_ret = gen_to_be_invoked.AppendActStart( _start_callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.ActSequence.AppendActStart!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InsertAtActStart(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Action<CsCat.Act>>(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    System.Action<CsCat.Act> _start = translator.GetDelegate<System.Action<CsCat.Act>>(L, 3);
                    bool _is_exit_at_once = LuaAPI.lua_toboolean(L, 4);
                    
                        CsCat.Act gen_ret = gen_to_be_invoked.InsertAtActStart( _index, _start, _is_exit_at_once );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Action<CsCat.Act>>(L, 3)) 
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    System.Action<CsCat.Act> _start = translator.GetDelegate<System.Action<CsCat.Act>>(L, 3);
                    
                        CsCat.Act gen_ret = gen_to_be_invoked.InsertAtActStart( _index, _start );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.ActSequence.InsertAtActStart!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Coroutine(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Collections.IEnumerator>(L, 2)) 
                {
                    System.Collections.IEnumerator _iEnumerator = (System.Collections.IEnumerator)translator.GetObject(L, 2, typeof(System.Collections.IEnumerator));
                    
                        CsCat.Act gen_ret = gen_to_be_invoked.Coroutine( _iEnumerator );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Coroutine>(L, 2)) 
                {
                    UnityEngine.Coroutine _coroutine = (UnityEngine.Coroutine)translator.GetObject(L, 2, typeof(UnityEngine.Coroutine));
                    
                        CsCat.Act gen_ret = gen_to_be_invoked.Coroutine( _coroutine );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.ActSequence.Coroutine!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WaitForSeconds(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _second = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        CsCat.Act gen_ret = gen_to_be_invoked.WaitForSeconds( _second );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WaitForFrames(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _frame_count = LuaAPI.xlua_tointeger(L, 2);
                    
                        CsCat.Act gen_ret = gen_to_be_invoked.WaitForFrames( _frame_count );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WaitForEndOfFrame(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        CsCat.Act gen_ret = gen_to_be_invoked.WaitForEndOfFrame(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WaitUntil(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<System.Func<bool>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Func<bool> _func = translator.GetDelegate<System.Func<bool>>(L, 2);
                    float _timeout = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        CsCat.Act gen_ret = gen_to_be_invoked.WaitUntil( _func, _timeout );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Func<bool>>(L, 2)) 
                {
                    System.Func<bool> _func = translator.GetDelegate<System.Func<bool>>(L, 2);
                    
                        CsCat.Act gen_ret = gen_to_be_invoked.WaitUntil( _func );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.ActSequence.WaitUntil!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnDespawn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnDespawn(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cur_act(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.cur_act);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_act_list(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.act_list);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cur_act_index(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.cur_act_index);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_act_list(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.act_list = (System.Collections.Generic.List<CsCat.Act>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<CsCat.Act>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cur_act_index(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.ActSequence gen_to_be_invoked = (CsCat.ActSequence)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.cur_act_index = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
