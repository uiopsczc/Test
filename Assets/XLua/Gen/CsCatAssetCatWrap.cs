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
    public class CsCatAssetCatWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CsCat.AssetCat);
			Utils.BeginObjectRegister(type, L, translator, 0, 19, 6, 4);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddOnLoadSuccessCallback", _m_AddOnLoadSuccessCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddOnLoadFailCallback", _m_AddOnLoadFailCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddOnLoadDoneCallback", _m_AddOnLoadDoneCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveOnLoadSuccessCallback", _m_RemoveOnLoadSuccessCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveOnLoadFailCallback", _m_RemoveOnLoadFailCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveOnLoadDoneCallback", _m_RemoveOnLoadDoneCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveAllOnLoadSuccessCallback", _m_RemoveAllOnLoadSuccessCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveAllOnLoadFailCallback", _m_RemoveAllOnLoadFailCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveAllOnLoadDoneCallback", _m_RemoveAllOnLoadDoneCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveCallback", _m_RemoveCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveAllCallback", _m_RemoveAllCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Get", _m_Get);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsLoadSuccess", _m_IsLoadSuccess);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsLoadFail", _m_IsLoadFail);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsLoadDone", _m_IsLoadDone);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddRefCount", _m_AddRefCount);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SubRefCount", _m_SubRefCount);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CheckNoRef", _m_CheckNoRef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAssets", _m_SetAssets);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "resultInfo", _g_get_resultInfo);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ref_count", _g_get_ref_count);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "asset_dict", _g_get_asset_dict);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "assetBundleCat", _g_get_assetBundleCat);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "asset_path", _g_get_asset_path);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "_resultInfo", _g_get__resultInfo);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "asset_dict", _s_set_asset_dict);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "assetBundleCat", _s_set_assetBundleCat);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "asset_path", _s_set_asset_path);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "_resultInfo", _s_set__resultInfo);
            
			
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
				if(LuaAPI.lua_gettop(L) == 2 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING))
				{
					string _asset_path = LuaAPI.lua_tostring(L, 2);
					
					CsCat.AssetCat gen_ret = new CsCat.AssetCat(_asset_path);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AssetCat constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddOnLoadSuccessCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 2)&& translator.Assignable<object>(L, 3)) 
                {
                    System.Action<CsCat.AssetCat> _on_load_success_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 2);
                    object _callback_cause = translator.GetObject(L, 3, typeof(object));
                    
                    gen_to_be_invoked.AddOnLoadSuccessCallback( _on_load_success_callback, _callback_cause );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 2)) 
                {
                    System.Action<CsCat.AssetCat> _on_load_success_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 2);
                    
                    gen_to_be_invoked.AddOnLoadSuccessCallback( _on_load_success_callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AssetCat.AddOnLoadSuccessCallback!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddOnLoadFailCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 2)&& translator.Assignable<object>(L, 3)) 
                {
                    System.Action<CsCat.AssetCat> _on_load_fail_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 2);
                    object _callback_cause = translator.GetObject(L, 3, typeof(object));
                    
                    gen_to_be_invoked.AddOnLoadFailCallback( _on_load_fail_callback, _callback_cause );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 2)) 
                {
                    System.Action<CsCat.AssetCat> _on_load_fail_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 2);
                    
                    gen_to_be_invoked.AddOnLoadFailCallback( _on_load_fail_callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AssetCat.AddOnLoadFailCallback!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddOnLoadDoneCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 2)&& translator.Assignable<object>(L, 3)) 
                {
                    System.Action<CsCat.AssetCat> _on_load_done_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 2);
                    object _callback_cause = translator.GetObject(L, 3, typeof(object));
                    
                    gen_to_be_invoked.AddOnLoadDoneCallback( _on_load_done_callback, _callback_cause );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 2)) 
                {
                    System.Action<CsCat.AssetCat> _on_load_done_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 2);
                    
                    gen_to_be_invoked.AddOnLoadDoneCallback( _on_load_done_callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AssetCat.AddOnLoadDoneCallback!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveOnLoadSuccessCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<object>(L, 2)) 
                {
                    object _callback_cause = translator.GetObject(L, 2, typeof(object));
                    
                    gen_to_be_invoked.RemoveOnLoadSuccessCallback( _callback_cause );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.RemoveOnLoadSuccessCallback(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 2)&& translator.Assignable<object>(L, 3)) 
                {
                    System.Action<CsCat.AssetCat> _on_load_success_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 2);
                    object _callback_cause = translator.GetObject(L, 3, typeof(object));
                    
                    gen_to_be_invoked.RemoveOnLoadSuccessCallback( _on_load_success_callback, _callback_cause );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 2)) 
                {
                    System.Action<CsCat.AssetCat> _on_load_success_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 2);
                    
                    gen_to_be_invoked.RemoveOnLoadSuccessCallback( _on_load_success_callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AssetCat.RemoveOnLoadSuccessCallback!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveOnLoadFailCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<object>(L, 2)) 
                {
                    object _callback_cause = translator.GetObject(L, 2, typeof(object));
                    
                    gen_to_be_invoked.RemoveOnLoadFailCallback( _callback_cause );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.RemoveOnLoadFailCallback(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 2)&& translator.Assignable<object>(L, 3)) 
                {
                    System.Action<CsCat.AssetCat> _on_load_fail_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 2);
                    object _callback_cause = translator.GetObject(L, 3, typeof(object));
                    
                    gen_to_be_invoked.RemoveOnLoadFailCallback( _on_load_fail_callback, _callback_cause );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 2)) 
                {
                    System.Action<CsCat.AssetCat> _on_load_fail_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 2);
                    
                    gen_to_be_invoked.RemoveOnLoadFailCallback( _on_load_fail_callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AssetCat.RemoveOnLoadFailCallback!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveOnLoadDoneCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<object>(L, 2)) 
                {
                    object _callback_cause = translator.GetObject(L, 2, typeof(object));
                    
                    gen_to_be_invoked.RemoveOnLoadDoneCallback( _callback_cause );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.RemoveOnLoadDoneCallback(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 2)&& translator.Assignable<object>(L, 3)) 
                {
                    System.Action<CsCat.AssetCat> _on_load_done_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 2);
                    object _callback_cause = translator.GetObject(L, 3, typeof(object));
                    
                    gen_to_be_invoked.RemoveOnLoadDoneCallback( _on_load_done_callback, _callback_cause );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 2)) 
                {
                    System.Action<CsCat.AssetCat> _on_load_done_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 2);
                    
                    gen_to_be_invoked.RemoveOnLoadDoneCallback( _on_load_done_callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AssetCat.RemoveOnLoadDoneCallback!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveAllOnLoadSuccessCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.RemoveAllOnLoadSuccessCallback(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveAllOnLoadFailCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.RemoveAllOnLoadFailCallback(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveAllOnLoadDoneCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.RemoveAllOnLoadDoneCallback(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<object>(L, 2)) 
                {
                    object _callback_cause = translator.GetObject(L, 2, typeof(object));
                    
                    gen_to_be_invoked.RemoveCallback( _callback_cause );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.RemoveCallback(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AssetCat.RemoveCallback!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveAllCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.RemoveAllCallback(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Get(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Type>(L, 3)) 
                {
                    string _sub_asset_path = LuaAPI.lua_tostring(L, 2);
                    System.Type _type = (System.Type)translator.GetObject(L, 3, typeof(System.Type));
                    
                        UnityEngine.Object gen_ret = gen_to_be_invoked.Get( _sub_asset_path, _type );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _sub_asset_path = LuaAPI.lua_tostring(L, 2);
                    
                        UnityEngine.Object gen_ret = gen_to_be_invoked.Get( _sub_asset_path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1) 
                {
                    
                        UnityEngine.Object gen_ret = gen_to_be_invoked.Get(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AssetCat.Get!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsLoadSuccess(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool gen_ret = gen_to_be_invoked.IsLoadSuccess(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsLoadFail(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool gen_ret = gen_to_be_invoked.IsLoadFail(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsLoadDone(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool gen_ret = gen_to_be_invoked.IsLoadDone(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddRefCount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.AddRefCount(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SubRefCount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    int _sub_value = LuaAPI.xlua_tointeger(L, 2);
                    bool _is_remove_asset_if_no_ref = LuaAPI.lua_toboolean(L, 3);
                    
                        int gen_ret = gen_to_be_invoked.SubRefCount( _sub_value, _is_remove_asset_if_no_ref );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _sub_value = LuaAPI.xlua_tointeger(L, 2);
                    
                        int gen_ret = gen_to_be_invoked.SubRefCount( _sub_value );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1) 
                {
                    
                        int gen_ret = gen_to_be_invoked.SubRefCount(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AssetCat.SubRefCount!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CheckNoRef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.CheckNoRef(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAssets(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Object[] _assets = (UnityEngine.Object[])translator.GetObject(L, 2, typeof(UnityEngine.Object[]));
                    
                    gen_to_be_invoked.SetAssets( _assets );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_resultInfo(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.resultInfo);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ref_count(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.ref_count);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_asset_dict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.asset_dict);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_assetBundleCat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.assetBundleCat);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_asset_path(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.asset_path);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get__resultInfo(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked._resultInfo);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_asset_dict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.asset_dict = (System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<System.Type, UnityEngine.Object>>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<System.Type, UnityEngine.Object>>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_assetBundleCat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.assetBundleCat = (CsCat.AssetBundleCat)translator.GetObject(L, 2, typeof(CsCat.AssetBundleCat));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_asset_path(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.asset_path = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set__resultInfo(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetCat gen_to_be_invoked = (CsCat.AssetCat)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked._resultInfo = (CsCat.ResultInfo)translator.GetObject(L, 2, typeof(CsCat.ResultInfo));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
