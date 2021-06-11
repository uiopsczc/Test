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
    public class CsCatUIManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CsCat.UIManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 14, 12, 9);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PostInit", _m_PostInit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddUIBlood", _m_AddUIBlood);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveUIBlood", _m_RemoveUIBlood);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Notify", _m_Notify);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LanternNotify", _m_LanternNotify);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HideFade", _m_HideFade);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FadeInOut", _m_FadeInOut);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FadeTo", _m_FadeTo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLoadingPct", _m_SetLoadingPct);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HideLoading", _m_HideLoading);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StartWaiting", _m_StartWaiting);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EndWaiting", _m_EndWaiting);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HideWaiting", _m_HideWaiting);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiCamera", _g_get_uiCamera);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiCanvas", _g_get_uiCanvas);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiCanvas_rectTransform", _g_get_uiCanvas_rectTransform);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiLayerManager", _g_get_uiLayerManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiNotifyManager", _g_get_uiNotifyManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiWaitingPanel", _g_get_uiWaitingPanel);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiLoadingPanel", _g_get_uiLoadingPanel);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiLanternNotifyPanel", _g_get_uiLanternNotifyPanel);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiShowLogoPanel", _g_get_uiShowLogoPanel);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiFadePanel", _g_get_uiFadePanel);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiBloodManager", _g_get_uiBloodManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiBlackMaskPanel", _g_get_uiBlackMaskPanel);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "uiLayerManager", _s_set_uiLayerManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uiNotifyManager", _s_set_uiNotifyManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uiWaitingPanel", _s_set_uiWaitingPanel);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uiLoadingPanel", _s_set_uiLoadingPanel);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uiLanternNotifyPanel", _s_set_uiLanternNotifyPanel);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uiShowLogoPanel", _s_set_uiShowLogoPanel);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uiFadePanel", _s_set_uiFadePanel);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uiBloodManager", _s_set_uiBloodManager);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uiBlackMaskPanel", _s_set_uiBlackMaskPanel);
            
			
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
					
					CsCat.UIManager gen_ret = new CsCat.UIManager();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.UIManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Init(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PostInit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.PostInit(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddUIBlood(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.Transform>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Nullable<int>>(L, 4)&& translator.Assignable<System.Nullable<float>>(L, 5)&& translator.Assignable<System.Collections.Generic.List<UnityEngine.Color>>(L, 6)) 
                {
                    UnityEngine.Transform _parent_transform = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    float _max_value = (float)LuaAPI.lua_tonumber(L, 3);
                    System.Nullable<int> _slider_count;translator.Get(L, 4, out _slider_count);
                    System.Nullable<float> _to_value;translator.Get(L, 5, out _to_value);
                    System.Collections.Generic.List<UnityEngine.Color> _slider_color_list = (System.Collections.Generic.List<UnityEngine.Color>)translator.GetObject(L, 6, typeof(System.Collections.Generic.List<UnityEngine.Color>));
                    
                        CsCat.UIBlood gen_ret = gen_to_be_invoked.AddUIBlood( _parent_transform, _max_value, _slider_count, _to_value, _slider_color_list );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Transform>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Nullable<int>>(L, 4)&& translator.Assignable<System.Nullable<float>>(L, 5)) 
                {
                    UnityEngine.Transform _parent_transform = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    float _max_value = (float)LuaAPI.lua_tonumber(L, 3);
                    System.Nullable<int> _slider_count;translator.Get(L, 4, out _slider_count);
                    System.Nullable<float> _to_value;translator.Get(L, 5, out _to_value);
                    
                        CsCat.UIBlood gen_ret = gen_to_be_invoked.AddUIBlood( _parent_transform, _max_value, _slider_count, _to_value );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.UIManager.AddUIBlood!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveUIBlood(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    CsCat.UIBlood _uiBlood = (CsCat.UIBlood)translator.GetObject(L, 2, typeof(CsCat.UIBlood));
                    
                    gen_to_be_invoked.RemoveUIBlood( _uiBlood );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Notify(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.Transform>(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    string _desc = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.Transform _parent_transform = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    bool _is_add_to_child_panel_stack = LuaAPI.lua_toboolean(L, 4);
                    
                    gen_to_be_invoked.Notify( _desc, _parent_transform, _is_add_to_child_panel_stack );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.Transform>(L, 3)) 
                {
                    string _desc = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.Transform _parent_transform = (UnityEngine.Transform)translator.GetObject(L, 3, typeof(UnityEngine.Transform));
                    
                    gen_to_be_invoked.Notify( _desc, _parent_transform );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _desc = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.Notify( _desc );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.UIManager.Notify!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LanternNotify(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _desc = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.LanternNotify( _desc );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HideFade(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.HideFade(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FadeInOut(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _duration = (float)LuaAPI.lua_tonumber(L, 2);
                    System.Action _callback = translator.GetDelegate<System.Action>(L, 3);
                    
                    gen_to_be_invoked.FadeInOut( _duration, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FadeTo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Action>(L, 4)) 
                {
                    float _toAplha = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    System.Action _callback = translator.GetDelegate<System.Action>(L, 4);
                    
                    gen_to_be_invoked.FadeTo( _toAplha, _duration, _callback );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float _toAplha = (float)LuaAPI.lua_tonumber(L, 2);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.FadeTo( _toAplha, _duration );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Action>(L, 5)) 
                {
                    float _fromAplha = (float)LuaAPI.lua_tonumber(L, 2);
                    float _toAplha = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    System.Action _callback = translator.GetDelegate<System.Action>(L, 5);
                    
                    gen_to_be_invoked.FadeTo( _fromAplha, _toAplha, _duration, _callback );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    float _fromAplha = (float)LuaAPI.lua_tonumber(L, 2);
                    float _toAplha = (float)LuaAPI.lua_tonumber(L, 3);
                    float _duration = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.FadeTo( _fromAplha, _toAplha, _duration );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.UIManager.FadeTo!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLoadingPct(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _pct = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.SetLoadingPct( _pct );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HideLoading(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.HideLoading(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartWaiting(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.StartWaiting(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EndWaiting(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.EndWaiting(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HideWaiting(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.HideWaiting(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiCamera(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiCamera);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiCanvas(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiCanvas);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiCanvas_rectTransform(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiCanvas_rectTransform);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiLayerManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiLayerManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiNotifyManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiNotifyManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiWaitingPanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiWaitingPanel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiLoadingPanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiLoadingPanel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiLanternNotifyPanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiLanternNotifyPanel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiShowLogoPanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiShowLogoPanel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiFadePanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiFadePanel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiBloodManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiBloodManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiBlackMaskPanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiBlackMaskPanel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uiLayerManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uiLayerManager = (CsCat.UILayerManager)translator.GetObject(L, 2, typeof(CsCat.UILayerManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uiNotifyManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uiNotifyManager = (CsCat.UINotifyManager)translator.GetObject(L, 2, typeof(CsCat.UINotifyManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uiWaitingPanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uiWaitingPanel = (CsCat.UIWaitingPanel)translator.GetObject(L, 2, typeof(CsCat.UIWaitingPanel));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uiLoadingPanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uiLoadingPanel = (CsCat.UILoadingPanel)translator.GetObject(L, 2, typeof(CsCat.UILoadingPanel));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uiLanternNotifyPanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uiLanternNotifyPanel = (CsCat.UILanternNotifyPanel)translator.GetObject(L, 2, typeof(CsCat.UILanternNotifyPanel));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uiShowLogoPanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uiShowLogoPanel = (CsCat.UIShowLogoPanel)translator.GetObject(L, 2, typeof(CsCat.UIShowLogoPanel));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uiFadePanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uiFadePanel = (CsCat.UIFadePanel)translator.GetObject(L, 2, typeof(CsCat.UIFadePanel));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uiBloodManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uiBloodManager = (CsCat.UIBloodManager)translator.GetObject(L, 2, typeof(CsCat.UIBloodManager));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uiBlackMaskPanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.UIManager gen_to_be_invoked = (CsCat.UIManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uiBlackMaskPanel = (CsCat.UIBlackMaskPanel)translator.GetObject(L, 2, typeof(CsCat.UIBlackMaskPanel));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
