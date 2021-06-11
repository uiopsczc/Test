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
    public class CsCatUILayerManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CsCat.UILayerManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 2, 2, 2);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetUILayer", _m_GetUILayer);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiLayer_dict", _g_get_uiLayer_dict);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiManager", _g_get_uiManager);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "uiLayer_dict", _s_set_uiLayer_dict);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uiManager", _s_set_uiManager);
            
			
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
					
					CsCat.UILayerManager gen_ret = new CsCat.UILayerManager();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.UILayerManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UILayerManager gen_to_be_invoked = (CsCat.UILayerManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    CsCat.UIManager _uiManager = (CsCat.UIManager)translator.GetObject(L, 2, typeof(CsCat.UIManager));
                    
                    gen_to_be_invoked.Init( _uiManager );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUILayer(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UILayerManager gen_to_be_invoked = (CsCat.UILayerManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _uiLayer_name = LuaAPI.lua_tostring(L, 2);
                    
                        CsCat.UILayer gen_ret = gen_to_be_invoked.GetUILayer( _uiLayer_name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<CsCat.EUILayerName>(L, 2)) 
                {
                    CsCat.EUILayerName _eUILayerName;translator.Get(L, 2, out _eUILayerName);
                    
                        CsCat.UILayer gen_ret = gen_to_be_invoked.GetUILayer( _eUILayerName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.UILayerManager.GetUILayer!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiLayer_dict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UILayerManager gen_to_be_invoked = (CsCat.UILayerManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiLayer_dict);
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
			
                CsCat.UILayerManager gen_to_be_invoked = (CsCat.UILayerManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uiLayer_dict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UILayerManager gen_to_be_invoked = (CsCat.UILayerManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uiLayer_dict = (System.Collections.Generic.Dictionary<CsCat.EUILayerName, CsCat.UILayer>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<CsCat.EUILayerName, CsCat.UILayer>));
            
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
			
                CsCat.UILayerManager gen_to_be_invoked = (CsCat.UILayerManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uiManager = (CsCat.UIManager)translator.GetObject(L, 2, typeof(CsCat.UIManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
