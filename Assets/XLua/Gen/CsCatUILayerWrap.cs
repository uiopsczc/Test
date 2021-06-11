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
    public class CsCatUILayerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CsCat.UILayer);
			Utils.BeginObjectRegister(type, L, translator, 0, 7, 2, 2);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Refresh", _m_Refresh);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemovePanel", _m_RemovePanel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddPanel", _m_AddPanel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPanelIndex", _m_SetPanelIndex);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPanelToTop", _m_SetPanelToTop);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPanelToBottom", _m_SetPanelToBottom);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiLayerConfig", _g_get_uiLayerConfig);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "panel_list", _g_get_panel_list);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "uiLayerConfig", _s_set_uiLayerConfig);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "panel_list", _s_set_panel_list);
            
			
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
					
					CsCat.UILayer gen_ret = new CsCat.UILayer();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.UILayer constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UILayer gen_to_be_invoked = (CsCat.UILayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _gameObject = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    CsCat.UILayerConfig _uiLayerConfig = (CsCat.UILayerConfig)translator.GetObject(L, 3, typeof(CsCat.UILayerConfig));
                    
                    gen_to_be_invoked.Init( _gameObject, _uiLayerConfig );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Refresh(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UILayer gen_to_be_invoked = (CsCat.UILayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Refresh(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemovePanel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UILayer gen_to_be_invoked = (CsCat.UILayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _panel = translator.GetObject(L, 2, typeof(object));
                    
                    gen_to_be_invoked.RemovePanel( _panel );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddPanel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UILayer gen_to_be_invoked = (CsCat.UILayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _panel = translator.GetObject(L, 2, typeof(object));
                    
                    gen_to_be_invoked.AddPanel( _panel );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPanelIndex(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UILayer gen_to_be_invoked = (CsCat.UILayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _panel = translator.GetObject(L, 2, typeof(object));
                    int _new_index = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.SetPanelIndex( _panel, _new_index );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPanelToTop(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UILayer gen_to_be_invoked = (CsCat.UILayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _panel = translator.GetObject(L, 2, typeof(object));
                    
                    gen_to_be_invoked.SetPanelToTop( _panel );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPanelToBottom(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UILayer gen_to_be_invoked = (CsCat.UILayer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _panel = translator.GetObject(L, 2, typeof(object));
                    
                    gen_to_be_invoked.SetPanelToBottom( _panel );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiLayerConfig(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UILayer gen_to_be_invoked = (CsCat.UILayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiLayerConfig);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_panel_list(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UILayer gen_to_be_invoked = (CsCat.UILayer)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.panel_list);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uiLayerConfig(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UILayer gen_to_be_invoked = (CsCat.UILayer)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uiLayerConfig = (CsCat.UILayerConfig)translator.GetObject(L, 2, typeof(CsCat.UILayerConfig));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_panel_list(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UILayer gen_to_be_invoked = (CsCat.UILayer)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.panel_list = (System.Collections.Generic.List<object>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<object>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
