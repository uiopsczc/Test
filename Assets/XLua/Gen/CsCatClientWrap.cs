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
    public class CsCatClientWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CsCat.Client);
			Utils.BeginObjectRegister(type, L, translator, 0, 7, 29, 27);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Start", _m_Start);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Test", _m_Test);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TestUser", _m_TestUser);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Rebort", _m_Rebort);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnApplicationQuit", _m_OnApplicationQuit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnApplicationPause", _m_OnApplicationPause);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "timerManager", _g_get_timerManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "eventDispatchers", _g_get_eventDispatchers);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "moveManager", _g_get_moveManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "assetBundleManager", _g_get_assetBundleManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "assetBundleUpdater", _g_get_assetBundleUpdater);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "audioManager", _g_get_audioManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "physicsManager", _g_get_physicsManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "commandManager", _g_get_commandManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "defaultInputManager", _g_get_defaultInputManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "frameCallbackMananger", _g_get_frameCallbackMananger);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "guidManager", _g_get_guidManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "randomManager", _g_get_randomManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "redDotManager", _g_get_redDotManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rpn", _g_get_rpn);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "combat", _g_get_combat);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "userFactory", _g_get_userFactory);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "roleFactory", _g_get_roleFactory);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "itemFactory", _g_get_itemFactory);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "missionFactory", _g_get_missionFactory);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "doerEventFactory", _g_get_doerEventFactory);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "sceneFactory", _g_get_sceneFactory);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "redDotLogic", _g_get_redDotLogic);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "user", _g_get_user);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "main_role", _g_get_main_role);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "stage", _g_get_stage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "syncUpdate", _g_get_syncUpdate);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "idPool", _g_get_idPool);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiManager", _g_get_uiManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "language", _g_get_language);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "moveManager", _s_set_moveManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "assetBundleManager", _s_set_assetBundleManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "assetBundleUpdater", _s_set_assetBundleUpdater);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "audioManager", _s_set_audioManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "physicsManager", _s_set_physicsManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "commandManager", _s_set_commandManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "defaultInputManager", _s_set_defaultInputManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "frameCallbackMananger", _s_set_frameCallbackMananger);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "guidManager", _s_set_guidManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "randomManager", _s_set_randomManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "redDotManager", _s_set_redDotManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rpn", _s_set_rpn);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "combat", _s_set_combat);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "userFactory", _s_set_userFactory);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "roleFactory", _s_set_roleFactory);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "itemFactory", _s_set_itemFactory);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "missionFactory", _s_set_missionFactory);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "doerEventFactory", _s_set_doerEventFactory);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "sceneFactory", _s_set_sceneFactory);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "redDotLogic", _s_set_redDotLogic);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "user", _s_set_user);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "main_role", _s_set_main_role);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "stage", _s_set_stage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "syncUpdate", _s_set_syncUpdate);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "idPool", _s_set_idPool);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uiManager", _s_set_uiManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "language", _s_set_language);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 1, 0);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "instance", _g_get_instance);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					CsCat.Client gen_ret = new CsCat.Client();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.Client constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _gameObject = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                    gen_to_be_invoked.Init( _gameObject );
                    
                    
                    
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
            
            
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Start(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Test(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Test(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TestUser(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.TestUser(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Rebort(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Rebort(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnApplicationQuit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.OnApplicationQuit(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnApplicationPause(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _is_paused = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.OnApplicationPause( _is_paused );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_timerManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.timerManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_instance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, CsCat.Client.instance);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_eventDispatchers(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.eventDispatchers);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_moveManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.moveManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_assetBundleManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.assetBundleManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_assetBundleUpdater(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.assetBundleUpdater);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_audioManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.audioManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_physicsManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.physicsManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_commandManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.commandManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultInputManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.defaultInputManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_frameCallbackMananger(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.frameCallbackMananger);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_guidManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.guidManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_randomManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.randomManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_redDotManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.redDotManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rpn(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rpn);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_combat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.combat);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_userFactory(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.userFactory);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_roleFactory(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.roleFactory);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_itemFactory(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.itemFactory);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_missionFactory(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.missionFactory);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_doerEventFactory(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.doerEventFactory);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_sceneFactory(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.sceneFactory);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_redDotLogic(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.redDotLogic);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_user(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.user);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_main_role(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.main_role);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_stage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.stage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_syncUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.syncUpdate);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_idPool(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.idPool);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_language(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.language);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_moveManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.moveManager = (CsCat.MoveManager)translator.GetObject(L, 2, typeof(CsCat.MoveManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_assetBundleManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.assetBundleManager = (CsCat.AssetBundleManager)translator.GetObject(L, 2, typeof(CsCat.AssetBundleManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_assetBundleUpdater(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.assetBundleUpdater = (CsCat.AssetBundleUpdater)translator.GetObject(L, 2, typeof(CsCat.AssetBundleUpdater));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_audioManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.audioManager = (CsCat.AudioManager)translator.GetObject(L, 2, typeof(CsCat.AudioManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_physicsManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.physicsManager = (CsCat.PhysicsManager)translator.GetObject(L, 2, typeof(CsCat.PhysicsManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_commandManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.commandManager = (CsCat.CommandManager)translator.GetObject(L, 2, typeof(CsCat.CommandManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_defaultInputManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.defaultInputManager = (CsCat.DefaultInputManager)translator.GetObject(L, 2, typeof(CsCat.DefaultInputManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_frameCallbackMananger(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.frameCallbackMananger = (CsCat.FrameCallbackMananger)translator.GetObject(L, 2, typeof(CsCat.FrameCallbackMananger));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_guidManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.guidManager = (CsCat.GuidManager)translator.GetObject(L, 2, typeof(CsCat.GuidManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_randomManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.randomManager = (CsCat.RandomManager)translator.GetObject(L, 2, typeof(CsCat.RandomManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_redDotManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.redDotManager = (CsCat.RedDotManager)translator.GetObject(L, 2, typeof(CsCat.RedDotManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rpn(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rpn = (CsCat.RPN)translator.GetObject(L, 2, typeof(CsCat.RPN));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_combat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.combat = (CsCat.CombatBase)translator.GetObject(L, 2, typeof(CsCat.CombatBase));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_userFactory(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.userFactory = (CsCat.UserFactory)translator.GetObject(L, 2, typeof(CsCat.UserFactory));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_roleFactory(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.roleFactory = (CsCat.RoleFactory)translator.GetObject(L, 2, typeof(CsCat.RoleFactory));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_itemFactory(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.itemFactory = (CsCat.ItemFactory)translator.GetObject(L, 2, typeof(CsCat.ItemFactory));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_missionFactory(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.missionFactory = (CsCat.MissionFactory)translator.GetObject(L, 2, typeof(CsCat.MissionFactory));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_doerEventFactory(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.doerEventFactory = (CsCat.DoerEventFactory)translator.GetObject(L, 2, typeof(CsCat.DoerEventFactory));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_sceneFactory(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.sceneFactory = (CsCat.SceneFactory)translator.GetObject(L, 2, typeof(CsCat.SceneFactory));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_redDotLogic(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.redDotLogic = (CsCat.RedDotLogic)translator.GetObject(L, 2, typeof(CsCat.RedDotLogic));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_user(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.user = (CsCat.User)translator.GetObject(L, 2, typeof(CsCat.User));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_main_role(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.main_role = (CsCat.Role)translator.GetObject(L, 2, typeof(CsCat.Role));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_stage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.stage = (CsCat.StageBase)translator.GetObject(L, 2, typeof(CsCat.StageBase));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_syncUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.syncUpdate = (CsCat.SyncUpdate)translator.GetObject(L, 2, typeof(CsCat.SyncUpdate));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_idPool(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.idPool = (CsCat.IdPool)translator.GetObject(L, 2, typeof(CsCat.IdPool));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uiManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uiManager = (CsCat.UIManager)translator.GetObject(L, 2, typeof(CsCat.UIManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_language(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.Client gen_to_be_invoked = (CsCat.Client)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.language = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
