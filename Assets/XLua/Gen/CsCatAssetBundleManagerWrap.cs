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
    public class CsCatAssetBundleManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CsCat.AssetBundleManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 15, 10, 6);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Initialize", _m_Initialize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Update", _m_Update);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetResidentAssets", _m_SetResidentAssets);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetOrLoadAssetCat", _m_GetOrLoadAssetCat);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadAssetAsync", _m_LoadAssetAsync);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddAssetCatOfNoRef", _m_AddAssetCatOfNoRef);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsAssetLoadSuccess", _m_IsAssetLoadSuccess);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetAssetCat", _m_GetAssetCat);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "__RemoveAssetCat", _m___RemoveAssetCat);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetAssetBundlePath", _m_GetAssetBundlePath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveAssetBundleCat", _m_RemoveAssetBundleCat);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetAssetBundleAsyncWebRequester", _m_GetAssetBundleAsyncWebRequester);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DownloadFileAsyncNoCache", _m_DownloadFileAsyncNoCache);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DownloadFileAsync", _m_DownloadFileAsync);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "download_url", _g_get_download_url);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "assetAsyncloader_prosessing_list", _g_get_assetAsyncloader_prosessing_list);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "assetBundleAsyncLoader_prosessing_list", _g_get_assetBundleAsyncLoader_prosessing_list);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "resourceWebRequester_all_dict", _g_get_resourceWebRequester_all_dict);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "asset_resident_dict", _g_get_asset_resident_dict);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "assetBundleCat_dict", _g_get_assetBundleCat_dict);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "assetCat_dict", _g_get_assetCat_dict);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "assetPathMap", _g_get_assetPathMap);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "assetBundleMap", _g_get_assetBundleMap);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "manifest", _g_get_manifest);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "asset_resident_dict", _s_set_asset_resident_dict);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "assetBundleCat_dict", _s_set_assetBundleCat_dict);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "assetCat_dict", _s_set_assetCat_dict);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "assetPathMap", _s_set_assetPathMap);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "assetBundleMap", _s_set_assetBundleMap);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "manifest", _s_set_manifest);
            
			
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
					
					CsCat.AssetBundleManager gen_ret = new CsCat.AssetBundleManager();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AssetBundleManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Init(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.Collections.IEnumerator gen_ret = gen_to_be_invoked.Initialize(  );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
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
            
            
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float _deltaTime = (float)LuaAPI.lua_tonumber(L, 2);
                    float _unscaledDeltaTime = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.Update( _deltaTime, _unscaledDeltaTime );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    float _deltaTime = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.Update( _deltaTime );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.Update(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AssetBundleManager.Update!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetResidentAssets(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count >= 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 3) || (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING))) 
                {
                    bool _is_resident = LuaAPI.lua_toboolean(L, 2);
                    string[] _asset_paths = translator.GetParams<string>(L, 3);
                    
                    gen_to_be_invoked.SetResidentAssets( _is_resident, _asset_paths );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Collections.Generic.List<string>>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    System.Collections.Generic.List<string> _asset_path_list = (System.Collections.Generic.List<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<string>));
                    bool _is_resident = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.SetResidentAssets( _asset_path_list, _is_resident );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Collections.Generic.List<string>>(L, 2)) 
                {
                    System.Collections.Generic.List<string> _asset_path_list = (System.Collections.Generic.List<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<string>));
                    
                    gen_to_be_invoked.SetResidentAssets( _asset_path_list );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AssetBundleManager.SetResidentAssets!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetOrLoadAssetCat(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 6&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 3)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 4)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 5)&& translator.Assignable<object>(L, 6)) 
                {
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    System.Action<CsCat.AssetCat> _on_load_success_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 3);
                    System.Action<CsCat.AssetCat> _on_load_fail_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 4);
                    System.Action<CsCat.AssetCat> _on_load_done_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 5);
                    object _callback_cause = translator.GetObject(L, 6, typeof(object));
                    
                        CsCat.AssetCat gen_ret = gen_to_be_invoked.GetOrLoadAssetCat( _asset_path, _on_load_success_callback, _on_load_fail_callback, _on_load_done_callback, _callback_cause );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 3)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 4)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 5)) 
                {
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    System.Action<CsCat.AssetCat> _on_load_success_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 3);
                    System.Action<CsCat.AssetCat> _on_load_fail_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 4);
                    System.Action<CsCat.AssetCat> _on_load_done_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 5);
                    
                        CsCat.AssetCat gen_ret = gen_to_be_invoked.GetOrLoadAssetCat( _asset_path, _on_load_success_callback, _on_load_fail_callback, _on_load_done_callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 3)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 4)) 
                {
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    System.Action<CsCat.AssetCat> _on_load_success_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 3);
                    System.Action<CsCat.AssetCat> _on_load_fail_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 4);
                    
                        CsCat.AssetCat gen_ret = gen_to_be_invoked.GetOrLoadAssetCat( _asset_path, _on_load_success_callback, _on_load_fail_callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 3)) 
                {
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    System.Action<CsCat.AssetCat> _on_load_success_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 3);
                    
                        CsCat.AssetCat gen_ret = gen_to_be_invoked.GetOrLoadAssetCat( _asset_path, _on_load_success_callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    
                        CsCat.AssetCat gen_ret = gen_to_be_invoked.GetOrLoadAssetCat( _asset_path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AssetBundleManager.GetOrLoadAssetCat!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadAssetAsync(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 6&& translator.Assignable<System.Collections.Generic.List<string>>(L, 2)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 3)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 4)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 5)&& translator.Assignable<object>(L, 6)) 
                {
                    System.Collections.Generic.List<string> _asset_path_list = (System.Collections.Generic.List<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<string>));
                    System.Action<CsCat.AssetCat> _on_load_success_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 3);
                    System.Action<CsCat.AssetCat> _on_load_fail_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 4);
                    System.Action<CsCat.AssetCat> _on_load_done_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 5);
                    object _callback_cause = translator.GetObject(L, 6, typeof(object));
                    
                        System.Collections.IEnumerator gen_ret = gen_to_be_invoked.LoadAssetAsync( _asset_path_list, _on_load_success_callback, _on_load_fail_callback, _on_load_done_callback, _callback_cause );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<System.Collections.Generic.List<string>>(L, 2)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 3)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 4)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 5)) 
                {
                    System.Collections.Generic.List<string> _asset_path_list = (System.Collections.Generic.List<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<string>));
                    System.Action<CsCat.AssetCat> _on_load_success_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 3);
                    System.Action<CsCat.AssetCat> _on_load_fail_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 4);
                    System.Action<CsCat.AssetCat> _on_load_done_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 5);
                    
                        System.Collections.IEnumerator gen_ret = gen_to_be_invoked.LoadAssetAsync( _asset_path_list, _on_load_success_callback, _on_load_fail_callback, _on_load_done_callback );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<System.Collections.Generic.List<string>>(L, 2)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 3)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 4)) 
                {
                    System.Collections.Generic.List<string> _asset_path_list = (System.Collections.Generic.List<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<string>));
                    System.Action<CsCat.AssetCat> _on_load_success_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 3);
                    System.Action<CsCat.AssetCat> _on_load_fail_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 4);
                    
                        System.Collections.IEnumerator gen_ret = gen_to_be_invoked.LoadAssetAsync( _asset_path_list, _on_load_success_callback, _on_load_fail_callback );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Collections.Generic.List<string>>(L, 2)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 3)) 
                {
                    System.Collections.Generic.List<string> _asset_path_list = (System.Collections.Generic.List<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<string>));
                    System.Action<CsCat.AssetCat> _on_load_success_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 3);
                    
                        System.Collections.IEnumerator gen_ret = gen_to_be_invoked.LoadAssetAsync( _asset_path_list, _on_load_success_callback );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Collections.Generic.List<string>>(L, 2)) 
                {
                    System.Collections.Generic.List<string> _asset_path_list = (System.Collections.Generic.List<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<string>));
                    
                        System.Collections.IEnumerator gen_ret = gen_to_be_invoked.LoadAssetAsync( _asset_path_list );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 3)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 4)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 5)&& translator.Assignable<object>(L, 6)) 
                {
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    System.Action<CsCat.AssetCat> _on_load_success_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 3);
                    System.Action<CsCat.AssetCat> _on_load_fail_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 4);
                    System.Action<CsCat.AssetCat> _on_load_done_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 5);
                    object _callback_cause = translator.GetObject(L, 6, typeof(object));
                    
                        CsCat.BaseAssetAsyncLoader gen_ret = gen_to_be_invoked.LoadAssetAsync( _asset_path, _on_load_success_callback, _on_load_fail_callback, _on_load_done_callback, _callback_cause );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 3)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 4)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 5)) 
                {
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    System.Action<CsCat.AssetCat> _on_load_success_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 3);
                    System.Action<CsCat.AssetCat> _on_load_fail_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 4);
                    System.Action<CsCat.AssetCat> _on_load_done_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 5);
                    
                        CsCat.BaseAssetAsyncLoader gen_ret = gen_to_be_invoked.LoadAssetAsync( _asset_path, _on_load_success_callback, _on_load_fail_callback, _on_load_done_callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 3)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 4)) 
                {
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    System.Action<CsCat.AssetCat> _on_load_success_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 3);
                    System.Action<CsCat.AssetCat> _on_load_fail_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 4);
                    
                        CsCat.BaseAssetAsyncLoader gen_ret = gen_to_be_invoked.LoadAssetAsync( _asset_path, _on_load_success_callback, _on_load_fail_callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Action<CsCat.AssetCat>>(L, 3)) 
                {
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    System.Action<CsCat.AssetCat> _on_load_success_callback = translator.GetDelegate<System.Action<CsCat.AssetCat>>(L, 3);
                    
                        CsCat.BaseAssetAsyncLoader gen_ret = gen_to_be_invoked.LoadAssetAsync( _asset_path, _on_load_success_callback );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    
                        CsCat.BaseAssetAsyncLoader gen_ret = gen_to_be_invoked.LoadAssetAsync( _asset_path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AssetBundleManager.LoadAssetAsync!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddAssetCatOfNoRef(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    CsCat.AssetCat _assetCat = (CsCat.AssetCat)translator.GetObject(L, 2, typeof(CsCat.AssetCat));
                    
                    gen_to_be_invoked.AddAssetCatOfNoRef( _assetCat );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsAssetLoadSuccess(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.IsAssetLoadSuccess( _asset_path );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAssetCat(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    
                        CsCat.AssetCat gen_ret = gen_to_be_invoked.GetAssetCat( _asset_path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m___RemoveAssetCat(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.__RemoveAssetCat( _asset_path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAssetBundlePath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.GetAssetBundlePath( _asset_path );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveAssetBundleCat(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _assetBundle_name = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.RemoveAssetBundleCat( _assetBundle_name );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<CsCat.AssetBundleCat>(L, 2)) 
                {
                    CsCat.AssetBundleCat _assetBundleCat = (CsCat.AssetBundleCat)translator.GetObject(L, 2, typeof(CsCat.AssetBundleCat));
                    
                    gen_to_be_invoked.RemoveAssetBundleCat( _assetBundleCat );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AssetBundleManager.RemoveAssetBundleCat!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAssetBundleAsyncWebRequester(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _assetBundle_name = LuaAPI.lua_tostring(L, 2);
                    
                        CsCat.ResourceWebRequester gen_ret = gen_to_be_invoked.GetAssetBundleAsyncWebRequester( _assetBundle_name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DownloadFileAsyncNoCache(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _url = LuaAPI.lua_tostring(L, 2);
                    
                        CsCat.ResourceWebRequester gen_ret = gen_to_be_invoked.DownloadFileAsyncNoCache( _url );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _download_url = LuaAPI.lua_tostring(L, 2);
                    string _file_path = LuaAPI.lua_tostring(L, 3);
                    
                        CsCat.ResourceWebRequester gen_ret = gen_to_be_invoked.DownloadFileAsyncNoCache( _download_url, _file_path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AssetBundleManager.DownloadFileAsyncNoCache!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DownloadFileAsync(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    string _url = LuaAPI.lua_tostring(L, 2);
                    int _max_reload_count = LuaAPI.xlua_tointeger(L, 3);
                    int _cur_reload_count = LuaAPI.xlua_tointeger(L, 4);
                    
                        System.Collections.IEnumerator gen_ret = gen_to_be_invoked.DownloadFileAsync( _url, _max_reload_count, _cur_reload_count );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string _url = LuaAPI.lua_tostring(L, 2);
                    int _max_reload_count = LuaAPI.xlua_tointeger(L, 3);
                    
                        System.Collections.IEnumerator gen_ret = gen_to_be_invoked.DownloadFileAsync( _url, _max_reload_count );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    string _download_url = LuaAPI.lua_tostring(L, 2);
                    string _file_path = LuaAPI.lua_tostring(L, 3);
                    int _max_reload_count = LuaAPI.xlua_tointeger(L, 4);
                    int _cur_reload_count = LuaAPI.xlua_tointeger(L, 5);
                    
                        System.Collections.IEnumerator gen_ret = gen_to_be_invoked.DownloadFileAsync( _download_url, _file_path, _max_reload_count, _cur_reload_count );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    string _download_url = LuaAPI.lua_tostring(L, 2);
                    string _file_path = LuaAPI.lua_tostring(L, 3);
                    int _max_reload_count = LuaAPI.xlua_tointeger(L, 4);
                    
                        System.Collections.IEnumerator gen_ret = gen_to_be_invoked.DownloadFileAsync( _download_url, _file_path, _max_reload_count );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AssetBundleManager.DownloadFileAsync!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_download_url(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.download_url);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_assetAsyncloader_prosessing_list(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.assetAsyncloader_prosessing_list);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_assetBundleAsyncLoader_prosessing_list(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.assetBundleAsyncLoader_prosessing_list);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_resourceWebRequester_all_dict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.resourceWebRequester_all_dict);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_asset_resident_dict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.asset_resident_dict);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_assetBundleCat_dict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.assetBundleCat_dict);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_assetCat_dict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.assetCat_dict);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_assetPathMap(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.assetPathMap);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_assetBundleMap(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.assetBundleMap);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_manifest(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.manifest);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_asset_resident_dict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.asset_resident_dict = (System.Collections.Generic.Dictionary<string, bool>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, bool>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_assetBundleCat_dict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.assetBundleCat_dict = (System.Collections.Generic.Dictionary<string, CsCat.AssetBundleCat>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, CsCat.AssetBundleCat>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_assetCat_dict(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.assetCat_dict = (System.Collections.Generic.Dictionary<string, CsCat.AssetCat>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, CsCat.AssetCat>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_assetPathMap(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.assetPathMap = (CsCat.AssetPathMap)translator.GetObject(L, 2, typeof(CsCat.AssetPathMap));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_assetBundleMap(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.assetBundleMap = (CsCat.AssetBundleMap)translator.GetObject(L, 2, typeof(CsCat.AssetBundleMap));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_manifest(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AssetBundleManager gen_to_be_invoked = (CsCat.AssetBundleManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.manifest = (CsCat.Manifest)translator.GetObject(L, 2, typeof(CsCat.Manifest));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
