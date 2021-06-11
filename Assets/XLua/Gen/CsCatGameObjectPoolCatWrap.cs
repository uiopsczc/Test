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
    public class CsCatGameObjectPoolCatWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CsCat.GameObjectPoolCat);
			Utils.BeginObjectRegister(type, L, translator, 0, 5, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Spawn", _m_Spawn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SpawnGameObject", _m_SpawnGameObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetPrefab", _m_GetPrefab);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitParent", _m_InitParent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Despawn", _m_Despawn);
			
			
			
			
			
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
				if(LuaAPI.lua_gettop(L) == 4 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && translator.Assignable<UnityEngine.GameObject>(L, 3) && (LuaAPI.lua_isnil(L, 4) || LuaAPI.lua_type(L, 4) == LuaTypes.LUA_TSTRING))
				{
					string _pool_name = LuaAPI.lua_tostring(L, 2);
					UnityEngine.GameObject _prefab = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
					string _category = LuaAPI.lua_tostring(L, 4);
					
					CsCat.GameObjectPoolCat gen_ret = new CsCat.GameObjectPoolCat(_pool_name, _prefab, _category);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 3 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && translator.Assignable<UnityEngine.GameObject>(L, 3))
				{
					string _pool_name = LuaAPI.lua_tostring(L, 2);
					UnityEngine.GameObject _prefab = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
					
					CsCat.GameObjectPoolCat gen_ret = new CsCat.GameObjectPoolCat(_pool_name, _prefab);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.GameObjectPoolCat constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Spawn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.GameObjectPoolCat gen_to_be_invoked = (CsCat.GameObjectPoolCat)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Action<object>>(L, 2)) 
                {
                    System.Action<object> _on_spawn_callback = translator.GetDelegate<System.Action<object>>(L, 2);
                    
                        object gen_ret = gen_to_be_invoked.Spawn( _on_spawn_callback );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1) 
                {
                    
                        object gen_ret = gen_to_be_invoked.Spawn(  );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.GameObjectPoolCat.Spawn!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SpawnGameObject(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.GameObjectPoolCat gen_to_be_invoked = (CsCat.GameObjectPoolCat)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Action<UnityEngine.GameObject>>(L, 2)) 
                {
                    System.Action<UnityEngine.GameObject> _on_spawn_callback = translator.GetDelegate<System.Action<UnityEngine.GameObject>>(L, 2);
                    
                        UnityEngine.GameObject gen_ret = gen_to_be_invoked.SpawnGameObject( _on_spawn_callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1) 
                {
                    
                        UnityEngine.GameObject gen_ret = gen_to_be_invoked.SpawnGameObject(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.GameObjectPoolCat.SpawnGameObject!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPrefab(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.GameObjectPoolCat gen_to_be_invoked = (CsCat.GameObjectPoolCat)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        UnityEngine.GameObject gen_ret = gen_to_be_invoked.GetPrefab(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitParent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.GameObjectPoolCat gen_to_be_invoked = (CsCat.GameObjectPoolCat)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Object _prefab = (UnityEngine.Object)translator.GetObject(L, 2, typeof(UnityEngine.Object));
                    string _category = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.InitParent( _prefab, _category );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Despawn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.GameObjectPoolCat gen_to_be_invoked = (CsCat.GameObjectPoolCat)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _obj = translator.GetObject(L, 2, typeof(object));
                    
                    gen_to_be_invoked.Despawn( _obj );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
