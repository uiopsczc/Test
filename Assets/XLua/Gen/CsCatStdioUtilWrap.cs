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
    public class CsCatStdioUtilWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CsCat.StdioUtil);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 24, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateYearlyFile", _m_CreateYearlyFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateMonthlyFile", _m_CreateMonthlyFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateDailyFile", _m_CreateDailyFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateHourlyFile", _m_CreateHourlyFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateTimesliceFile", _m_CreateTimesliceFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetExtName", _m_GetExtName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveExtName", _m_RemoveExtName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ChangeExtName", _m_ChangeExtName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadStream", _m_ReadStream_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CopyStream", _m_CopyStream_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WriteFile", _m_WriteFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadFile", _m_ReadFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WriteTextFile", _m_WriteTextFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadTextFile", _m_ReadTextFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadUrl", _m_ReadUrl_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CopyFile", _m_CopyFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveFiles", _m_RemoveFiles_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveFile", _m_RemoveFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ClearDir", _m_ClearDir_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SearchFiles", _m_SearchFiles_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateDirectoryIfNotExist", _m_CreateDirectoryIfNotExist_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateFileIfNotExist", _m_CreateFileIfNotExist_xlua_st_);
            
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DIR_FILTER", CsCat.StdioUtil.DIR_FILTER);
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					CsCat.StdioUtil gen_ret = new CsCat.StdioUtil();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.StdioUtil constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateYearlyFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<System.IO.DirectoryInfo>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& translator.Assignable<CsCat.RandomManager>(L, 4)) 
                {
                    System.IO.DirectoryInfo _dir = (System.IO.DirectoryInfo)translator.GetObject(L, 1, typeof(System.IO.DirectoryInfo));
                    string _prefix = LuaAPI.lua_tostring(L, 2);
                    string _suffix = LuaAPI.lua_tostring(L, 3);
                    CsCat.RandomManager _randomManager = (CsCat.RandomManager)translator.GetObject(L, 4, typeof(CsCat.RandomManager));
                    
                        System.IO.FileInfo gen_ret = CsCat.StdioUtil.CreateYearlyFile( _dir, _prefix, _suffix, _randomManager );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.IO.DirectoryInfo>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    System.IO.DirectoryInfo _dir = (System.IO.DirectoryInfo)translator.GetObject(L, 1, typeof(System.IO.DirectoryInfo));
                    string _prefix = LuaAPI.lua_tostring(L, 2);
                    string _suffix = LuaAPI.lua_tostring(L, 3);
                    
                        System.IO.FileInfo gen_ret = CsCat.StdioUtil.CreateYearlyFile( _dir, _prefix, _suffix );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.StdioUtil.CreateYearlyFile!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateMonthlyFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<System.IO.DirectoryInfo>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& translator.Assignable<CsCat.RandomManager>(L, 4)) 
                {
                    System.IO.DirectoryInfo _dir = (System.IO.DirectoryInfo)translator.GetObject(L, 1, typeof(System.IO.DirectoryInfo));
                    string _prefix = LuaAPI.lua_tostring(L, 2);
                    string _suffix = LuaAPI.lua_tostring(L, 3);
                    CsCat.RandomManager _randomManager = (CsCat.RandomManager)translator.GetObject(L, 4, typeof(CsCat.RandomManager));
                    
                        System.IO.FileInfo gen_ret = CsCat.StdioUtil.CreateMonthlyFile( _dir, _prefix, _suffix, _randomManager );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.IO.DirectoryInfo>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    System.IO.DirectoryInfo _dir = (System.IO.DirectoryInfo)translator.GetObject(L, 1, typeof(System.IO.DirectoryInfo));
                    string _prefix = LuaAPI.lua_tostring(L, 2);
                    string _suffix = LuaAPI.lua_tostring(L, 3);
                    
                        System.IO.FileInfo gen_ret = CsCat.StdioUtil.CreateMonthlyFile( _dir, _prefix, _suffix );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.StdioUtil.CreateMonthlyFile!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateDailyFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<System.IO.DirectoryInfo>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& translator.Assignable<CsCat.RandomManager>(L, 4)) 
                {
                    System.IO.DirectoryInfo _dir = (System.IO.DirectoryInfo)translator.GetObject(L, 1, typeof(System.IO.DirectoryInfo));
                    string _prefix = LuaAPI.lua_tostring(L, 2);
                    string _suffix = LuaAPI.lua_tostring(L, 3);
                    CsCat.RandomManager _randomManager = (CsCat.RandomManager)translator.GetObject(L, 4, typeof(CsCat.RandomManager));
                    
                        System.IO.FileInfo gen_ret = CsCat.StdioUtil.CreateDailyFile( _dir, _prefix, _suffix, _randomManager );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.IO.DirectoryInfo>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    System.IO.DirectoryInfo _dir = (System.IO.DirectoryInfo)translator.GetObject(L, 1, typeof(System.IO.DirectoryInfo));
                    string _prefix = LuaAPI.lua_tostring(L, 2);
                    string _suffix = LuaAPI.lua_tostring(L, 3);
                    
                        System.IO.FileInfo gen_ret = CsCat.StdioUtil.CreateDailyFile( _dir, _prefix, _suffix );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.StdioUtil.CreateDailyFile!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateHourlyFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<System.IO.DirectoryInfo>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& translator.Assignable<CsCat.RandomManager>(L, 4)) 
                {
                    System.IO.DirectoryInfo _dir = (System.IO.DirectoryInfo)translator.GetObject(L, 1, typeof(System.IO.DirectoryInfo));
                    string _prefix = LuaAPI.lua_tostring(L, 2);
                    string _suffix = LuaAPI.lua_tostring(L, 3);
                    CsCat.RandomManager _randomManager = (CsCat.RandomManager)translator.GetObject(L, 4, typeof(CsCat.RandomManager));
                    
                        System.IO.FileInfo gen_ret = CsCat.StdioUtil.CreateHourlyFile( _dir, _prefix, _suffix, _randomManager );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.IO.DirectoryInfo>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    System.IO.DirectoryInfo _dir = (System.IO.DirectoryInfo)translator.GetObject(L, 1, typeof(System.IO.DirectoryInfo));
                    string _prefix = LuaAPI.lua_tostring(L, 2);
                    string _suffix = LuaAPI.lua_tostring(L, 3);
                    
                        System.IO.FileInfo gen_ret = CsCat.StdioUtil.CreateHourlyFile( _dir, _prefix, _suffix );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.StdioUtil.CreateHourlyFile!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateTimesliceFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<System.IO.DirectoryInfo>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& translator.Assignable<CsCat.RandomManager>(L, 4)) 
                {
                    System.IO.DirectoryInfo _dir = (System.IO.DirectoryInfo)translator.GetObject(L, 1, typeof(System.IO.DirectoryInfo));
                    string _prefix = LuaAPI.lua_tostring(L, 2);
                    string _suffix = LuaAPI.lua_tostring(L, 3);
                    CsCat.RandomManager _randomManager = (CsCat.RandomManager)translator.GetObject(L, 4, typeof(CsCat.RandomManager));
                    
                        System.IO.FileInfo gen_ret = CsCat.StdioUtil.CreateTimesliceFile( _dir, _prefix, _suffix, _randomManager );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.IO.DirectoryInfo>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    System.IO.DirectoryInfo _dir = (System.IO.DirectoryInfo)translator.GetObject(L, 1, typeof(System.IO.DirectoryInfo));
                    string _prefix = LuaAPI.lua_tostring(L, 2);
                    string _suffix = LuaAPI.lua_tostring(L, 3);
                    
                        System.IO.FileInfo gen_ret = CsCat.StdioUtil.CreateTimesliceFile( _dir, _prefix, _suffix );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.StdioUtil.CreateTimesliceFile!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetExtName_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = CsCat.StdioUtil.GetExtName( _name );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveExtName_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = CsCat.StdioUtil.RemoveExtName( _name );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeExtName_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 1);
                    string _ext_name = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = CsCat.StdioUtil.ChangeExtName( _name, _ext_name );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadStream_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<System.IO.Stream>(L, 1)) 
                {
                    System.IO.Stream _ins = (System.IO.Stream)translator.GetObject(L, 1, typeof(System.IO.Stream));
                    
                        byte[] gen_ret = CsCat.StdioUtil.ReadStream( _ins );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.IO.Stream>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    System.IO.Stream _ins = (System.IO.Stream)translator.GetObject(L, 1, typeof(System.IO.Stream));
                    int _len = LuaAPI.xlua_tointeger(L, 2);
                    
                        byte[] gen_ret = CsCat.StdioUtil.ReadStream( _ins, _len );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.IO.Stream>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    System.IO.Stream _ins = (System.IO.Stream)translator.GetObject(L, 1, typeof(System.IO.Stream));
                    byte[] _buf = LuaAPI.lua_tobytes(L, 2);
                    
                        int gen_ret = CsCat.StdioUtil.ReadStream( _ins, _buf );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<System.IO.Stream>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    System.IO.Stream _ins = (System.IO.Stream)translator.GetObject(L, 1, typeof(System.IO.Stream));
                    byte[] _buf = LuaAPI.lua_tobytes(L, 2);
                    int _offset = LuaAPI.xlua_tointeger(L, 3);
                    int _len = LuaAPI.xlua_tointeger(L, 4);
                    
                        int gen_ret = CsCat.StdioUtil.ReadStream( _ins, _buf, _offset, _len );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.StdioUtil.ReadStream!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CopyStream_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.IO.Stream _ins = (System.IO.Stream)translator.GetObject(L, 1, typeof(System.IO.Stream));
                    System.IO.Stream _outs = (System.IO.Stream)translator.GetObject(L, 2, typeof(System.IO.Stream));
                    
                    CsCat.StdioUtil.CopyStream( _ins, _outs );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _file_name = LuaAPI.lua_tostring(L, 1);
                    byte[] _data = LuaAPI.lua_tobytes(L, 2);
                    
                    CsCat.StdioUtil.WriteFile( _file_name, _data );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.IO.FileInfo>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    System.IO.FileInfo _file = (System.IO.FileInfo)translator.GetObject(L, 1, typeof(System.IO.FileInfo));
                    byte[] _data = LuaAPI.lua_tobytes(L, 2);
                    
                    CsCat.StdioUtil.WriteFile( _file, _data );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _file_name = LuaAPI.lua_tostring(L, 1);
                    byte[] _data = LuaAPI.lua_tobytes(L, 2);
                    bool _append = LuaAPI.lua_toboolean(L, 3);
                    
                    CsCat.StdioUtil.WriteFile( _file_name, _data, _append );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<System.IO.FileInfo>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    System.IO.FileInfo _file = (System.IO.FileInfo)translator.GetObject(L, 1, typeof(System.IO.FileInfo));
                    byte[] _data = LuaAPI.lua_tobytes(L, 2);
                    bool _append = LuaAPI.lua_toboolean(L, 3);
                    
                    CsCat.StdioUtil.WriteFile( _file, _data, _append );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.StdioUtil.WriteFile!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _file_name = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] gen_ret = CsCat.StdioUtil.ReadFile( _file_name );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.IO.FileInfo>(L, 1)) 
                {
                    System.IO.FileInfo _file = (System.IO.FileInfo)translator.GetObject(L, 1, typeof(System.IO.FileInfo));
                    
                        byte[] gen_ret = CsCat.StdioUtil.ReadFile( _file );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.StdioUtil.ReadFile!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteTextFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<System.IO.FileInfo>(L, 1)&& translator.Assignable<System.Collections.Generic.List<string>>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    System.IO.FileInfo _file = (System.IO.FileInfo)translator.GetObject(L, 1, typeof(System.IO.FileInfo));
                    System.Collections.Generic.List<string> _content_list = (System.Collections.Generic.List<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<string>));
                    bool _is_append = LuaAPI.lua_toboolean(L, 3);
                    
                    CsCat.StdioUtil.WriteTextFile( _file, _content_list, _is_append );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    string _file_name = LuaAPI.lua_tostring(L, 1);
                    string _content = LuaAPI.lua_tostring(L, 2);
                    bool _is_writeLine = LuaAPI.lua_toboolean(L, 3);
                    bool _is_append = LuaAPI.lua_toboolean(L, 4);
                    
                    CsCat.StdioUtil.WriteTextFile( _file_name, _content, _is_writeLine, _is_append );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _file_name = LuaAPI.lua_tostring(L, 1);
                    string _content = LuaAPI.lua_tostring(L, 2);
                    bool _is_writeLine = LuaAPI.lua_toboolean(L, 3);
                    
                    CsCat.StdioUtil.WriteTextFile( _file_name, _content, _is_writeLine );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _file_name = LuaAPI.lua_tostring(L, 1);
                    string _content = LuaAPI.lua_tostring(L, 2);
                    
                    CsCat.StdioUtil.WriteTextFile( _file_name, _content );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<System.IO.FileInfo>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    System.IO.FileInfo _file = (System.IO.FileInfo)translator.GetObject(L, 1, typeof(System.IO.FileInfo));
                    string _content = LuaAPI.lua_tostring(L, 2);
                    bool _is_writeLine = LuaAPI.lua_toboolean(L, 3);
                    bool _is_append = LuaAPI.lua_toboolean(L, 4);
                    
                    CsCat.StdioUtil.WriteTextFile( _file, _content, _is_writeLine, _is_append );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.StdioUtil.WriteTextFile!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadTextFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _file_name = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = CsCat.StdioUtil.ReadTextFile( _file_name );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.IO.FileInfo>(L, 1)) 
                {
                    System.IO.FileInfo _file = (System.IO.FileInfo)translator.GetObject(L, 1, typeof(System.IO.FileInfo));
                    
                        string gen_ret = CsCat.StdioUtil.ReadTextFile( _file );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.StdioUtil.ReadTextFile!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadUrl_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _url = LuaAPI.lua_tostring(L, 1);
                    int _retry_count = LuaAPI.xlua_tointeger(L, 2);
                    int _err_wait_duration = LuaAPI.xlua_tointeger(L, 3);
                    
                        byte[] gen_ret = CsCat.StdioUtil.ReadUrl( _url, _retry_count, _err_wait_duration );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CopyFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.IO.FileSystemInfo _src = (System.IO.FileSystemInfo)translator.GetObject(L, 1, typeof(System.IO.FileSystemInfo));
                    System.IO.FileSystemInfo _dst = (System.IO.FileSystemInfo)translator.GetObject(L, 2, typeof(System.IO.FileSystemInfo));
                    
                    CsCat.StdioUtil.CopyFile( _src, _dst );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveFiles_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                    CsCat.StdioUtil.RemoveFiles( _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.IO.FileSystemInfo _file = (System.IO.FileSystemInfo)translator.GetObject(L, 1, typeof(System.IO.FileSystemInfo));
                    
                    CsCat.StdioUtil.RemoveFile( _file );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearDir_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _dir = LuaAPI.lua_tostring(L, 1);
                    
                    CsCat.StdioUtil.ClearDir( _dir );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& translator.Assignable<System.IO.DirectoryInfo>(L, 1)) 
                {
                    System.IO.DirectoryInfo _dir = (System.IO.DirectoryInfo)translator.GetObject(L, 1, typeof(System.IO.DirectoryInfo));
                    
                    CsCat.StdioUtil.ClearDir( _dir );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.StdioUtil.ClearDir!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SearchFiles_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.IO.DirectoryInfo _dir = (System.IO.DirectoryInfo)translator.GetObject(L, 1, typeof(System.IO.DirectoryInfo));
                    CsCat.IFileSystemInfoFilter _filter = (CsCat.IFileSystemInfoFilter)translator.GetObject(L, 2, typeof(CsCat.IFileSystemInfoFilter));
                    System.Collections.IList _results = (System.Collections.IList)translator.GetObject(L, 3, typeof(System.Collections.IList));
                    
                    CsCat.StdioUtil.SearchFiles( _dir, _filter, _results );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateDirectoryIfNotExist_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                    CsCat.StdioUtil.CreateDirectoryIfNotExist( _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateFileIfNotExist_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                    CsCat.StdioUtil.CreateFileIfNotExist( _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
