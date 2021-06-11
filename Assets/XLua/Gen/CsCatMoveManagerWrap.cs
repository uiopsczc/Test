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
    public class CsCatMoveManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CsCat.MoveManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 4, 3, 3);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MoveTo", _m_MoveTo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StopMoveTo", _m_StopMoveTo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Follow", _m_Follow);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StopFollow", _m_StopFollow);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "moveInfo_dict", _g_get_moveInfo_dict);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "followInfo_dict", _g_get_followInfo_dict);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "delete_cache", _g_get_delete_cache);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "moveInfo_dict", _s_set_moveInfo_dict);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "followInfo_dict", _s_set_followInfo_dict);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "delete_cache", _s_set_delete_cache);
            
			
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
					
					CsCat.MoveManager gen_ret = new CsCat.MoveManager();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.MoveManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MoveTo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.MoveManager gen_to_be_invoked = (CsCat.MoveManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Transform _transform = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    UnityEngine.Vector3 _to_pos;translator.Get(L, 3, out _to_pos);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.MoveTo( _transform, _to_pos, _duration );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopMoveTo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.MoveManager gen_to_be_invoked = (CsCat.MoveManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Transform _transfrom = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
                    gen_to_be_invoked.StopMoveTo( _transfrom );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Follow(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.MoveManager gen_to_be_invoked = (CsCat.MoveManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Transform _transform = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    UnityEngine.Transform _follow_transform = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    
                    gen_to_be_invoked.Follow( _transform, _follow_transform );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopFollow(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.MoveManager gen_to_be_invoked = (CsCat.MoveManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Transform _transform = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    
                    gen_to_be_invoked.StopFollow( _transform );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_moveInfo_dict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.MoveManager gen_to_be_invoked = (CsCat.MoveManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.moveInfo_dict);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_followInfo_dict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.MoveManager gen_to_be_invoked = (CsCat.MoveManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.followInfo_dict);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_delete_cache(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.MoveManager gen_to_be_invoked = (CsCat.MoveManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.delete_cache);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_moveInfo_dict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.MoveManager gen_to_be_invoked = (CsCat.MoveManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.moveInfo_dict = (System.Collections.Generic.Dictionary<UnityEngine.Transform, CsCat.MoveInfo>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<UnityEngine.Transform, CsCat.MoveInfo>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_followInfo_dict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.MoveManager gen_to_be_invoked = (CsCat.MoveManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.followInfo_dict = (System.Collections.Generic.Dictionary<UnityEngine.Transform, CsCat.FollowInfo>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<UnityEngine.Transform, CsCat.FollowInfo>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_delete_cache(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.MoveManager gen_to_be_invoked = (CsCat.MoveManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.delete_cache = (System.Collections.Generic.List<UnityEngine.Transform>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Transform>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
