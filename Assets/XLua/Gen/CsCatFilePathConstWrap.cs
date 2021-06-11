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
    public class CsCatFilePathConstWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CsCat.FilePathConst);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 4, 19, 16);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "GetPathStartWithRelativePath", _m_GetPathStartWithRelativePath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetPathRelativeTo", _m_GetPathRelativeTo_xlua_st_);
            
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "File_Prefix", CsCat.FilePathConst.File_Prefix);
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "AssetBundlesBuildOutputPath", _g_get_AssetBundlesBuildOutputPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "RootPaths", _g_get_RootPaths);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "ExternalPath", _g_get_ExternalPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "ProjectPath", _g_get_ProjectPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "DataPath", _g_get_DataPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "AssetsPath", _g_get_AssetsPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "StreamingAssetsPath", _g_get_StreamingAssetsPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "PersistentDataPath", _g_get_PersistentDataPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "AssetBundlesPath", _g_get_AssetBundlesPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "ResourcesPath", _g_get_ResourcesPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Resources_Flag", _g_get_Resources_Flag);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "SpritesPath", _g_get_SpritesPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "AssetBundlesMainfest", _g_get_AssetBundlesMainfest);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "ExesPath", _g_get_ExesPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "ExternalScriptsPath", _g_get_ExternalScriptsPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "PersistentAssetBundleRoot", _g_get_PersistentAssetBundleRoot);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "EditorAssetsPath", _g_get_EditorAssetsPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "ExcelsPath", _g_get_ExcelsPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "ExcelAssetsPath", _g_get_ExcelAssetsPath);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "ProjectPath", _s_set_ProjectPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "DataPath", _s_set_DataPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "AssetsPath", _s_set_AssetsPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "StreamingAssetsPath", _s_set_StreamingAssetsPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "PersistentDataPath", _s_set_PersistentDataPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "AssetBundlesPath", _s_set_AssetBundlesPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "ResourcesPath", _s_set_ResourcesPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Resources_Flag", _s_set_Resources_Flag);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "SpritesPath", _s_set_SpritesPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "AssetBundlesMainfest", _s_set_AssetBundlesMainfest);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "ExesPath", _s_set_ExesPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "ExternalScriptsPath", _s_set_ExternalScriptsPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "PersistentAssetBundleRoot", _s_set_PersistentAssetBundleRoot);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "EditorAssetsPath", _s_set_EditorAssetsPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "ExcelsPath", _s_set_ExcelsPath);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "ExcelAssetsPath", _s_set_ExcelAssetsPath);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "CsCat.FilePathConst does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPathStartWithRelativePath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _relative_path = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = CsCat.FilePathConst.GetPathStartWithRelativePath( _path, _relative_path );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPathRelativeTo_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _relative_path = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = CsCat.FilePathConst.GetPathRelativeTo( _path, _relative_path );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AssetBundlesBuildOutputPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.FilePathConst.AssetBundlesBuildOutputPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RootPaths(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, CsCat.FilePathConst.RootPaths);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ExternalPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.FilePathConst.ExternalPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ProjectPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.FilePathConst.ProjectPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DataPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.FilePathConst.DataPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AssetsPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.FilePathConst.AssetsPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_StreamingAssetsPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.FilePathConst.StreamingAssetsPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PersistentDataPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.FilePathConst.PersistentDataPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AssetBundlesPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.FilePathConst.AssetBundlesPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ResourcesPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.FilePathConst.ResourcesPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Resources_Flag(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.FilePathConst.Resources_Flag);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SpritesPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.FilePathConst.SpritesPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AssetBundlesMainfest(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.FilePathConst.AssetBundlesMainfest);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ExesPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.FilePathConst.ExesPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ExternalScriptsPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.FilePathConst.ExternalScriptsPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PersistentAssetBundleRoot(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.FilePathConst.PersistentAssetBundleRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_EditorAssetsPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.FilePathConst.EditorAssetsPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ExcelsPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.FilePathConst.ExcelsPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ExcelAssetsPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.FilePathConst.ExcelAssetsPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ProjectPath(RealStatePtr L)
        {
		    try {
                
			    CsCat.FilePathConst.ProjectPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DataPath(RealStatePtr L)
        {
		    try {
                
			    CsCat.FilePathConst.DataPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AssetsPath(RealStatePtr L)
        {
		    try {
                
			    CsCat.FilePathConst.AssetsPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_StreamingAssetsPath(RealStatePtr L)
        {
		    try {
                
			    CsCat.FilePathConst.StreamingAssetsPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_PersistentDataPath(RealStatePtr L)
        {
		    try {
                
			    CsCat.FilePathConst.PersistentDataPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AssetBundlesPath(RealStatePtr L)
        {
		    try {
                
			    CsCat.FilePathConst.AssetBundlesPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ResourcesPath(RealStatePtr L)
        {
		    try {
                
			    CsCat.FilePathConst.ResourcesPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Resources_Flag(RealStatePtr L)
        {
		    try {
                
			    CsCat.FilePathConst.Resources_Flag = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SpritesPath(RealStatePtr L)
        {
		    try {
                
			    CsCat.FilePathConst.SpritesPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AssetBundlesMainfest(RealStatePtr L)
        {
		    try {
                
			    CsCat.FilePathConst.AssetBundlesMainfest = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ExesPath(RealStatePtr L)
        {
		    try {
                
			    CsCat.FilePathConst.ExesPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ExternalScriptsPath(RealStatePtr L)
        {
		    try {
                
			    CsCat.FilePathConst.ExternalScriptsPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_PersistentAssetBundleRoot(RealStatePtr L)
        {
		    try {
                
			    CsCat.FilePathConst.PersistentAssetBundleRoot = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_EditorAssetsPath(RealStatePtr L)
        {
		    try {
                
			    CsCat.FilePathConst.EditorAssetsPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ExcelsPath(RealStatePtr L)
        {
		    try {
                
			    CsCat.FilePathConst.ExcelsPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ExcelAssetsPath(RealStatePtr L)
        {
		    try {
                
			    CsCat.FilePathConst.ExcelAssetsPath = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
