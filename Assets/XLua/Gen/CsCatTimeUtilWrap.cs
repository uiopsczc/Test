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
    public class CsCatTimeUtilWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CsCat.TimeUtil);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 7, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "SystemTicksToSecond", _m_SystemTicksToSecond_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SecondToSystemTicks", _m_SecondToSystemTicks_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SecondToTimeTable", _m_SecondToTimeTable_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SecondToStringHHmmss", _m_SecondToStringHHmmss_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DiffBySeconds", _m_DiffBySeconds_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetNowTimestamp", _m_GetNowTimestamp_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					CsCat.TimeUtil gen_ret = new CsCat.TimeUtil();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.TimeUtil constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SystemTicksToSecond_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    long _tick = LuaAPI.lua_toint64(L, 1);
                    
                        long gen_ret = CsCat.TimeUtil.SystemTicksToSecond( _tick );
                        LuaAPI.lua_pushint64(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SecondToSystemTicks_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    double _second = LuaAPI.lua_tonumber(L, 1);
                    
                        long gen_ret = CsCat.TimeUtil.SecondToSystemTicks( _second );
                        LuaAPI.lua_pushint64(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SecondToTimeTable_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    long _seconds = LuaAPI.lua_toint64(L, 1);
                    
                        CsCat.TimeTable gen_ret = CsCat.TimeUtil.SecondToTimeTable( _seconds );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SecondToStringHHmmss_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1) || LuaAPI.lua_isint64(L, 1))&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    long _seconds = LuaAPI.lua_toint64(L, 1);
                    int _h_count = LuaAPI.xlua_tointeger(L, 2);
                    bool _is_zero_ingroe = LuaAPI.lua_toboolean(L, 3);
                    
                        string gen_ret = CsCat.TimeUtil.SecondToStringHHmmss( _seconds, _h_count, _is_zero_ingroe );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1) || LuaAPI.lua_isint64(L, 1))&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    long _seconds = LuaAPI.lua_toint64(L, 1);
                    int _h_count = LuaAPI.xlua_tointeger(L, 2);
                    
                        string gen_ret = CsCat.TimeUtil.SecondToStringHHmmss( _seconds, _h_count );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1) || LuaAPI.lua_isint64(L, 1))) 
                {
                    long _seconds = LuaAPI.lua_toint64(L, 1);
                    
                        string gen_ret = CsCat.TimeUtil.SecondToStringHHmmss( _seconds );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.TimeUtil.SecondToStringHHmmss!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DiffBySeconds_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    long _t1 = LuaAPI.lua_toint64(L, 1);
                    long _t2 = LuaAPI.lua_toint64(L, 2);
                    
                        float gen_ret = CsCat.TimeUtil.DiffBySeconds( _t1, _t2 );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNowTimestamp_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        long gen_ret = CsCat.TimeUtil.GetNowTimestamp(  );
                        LuaAPI.lua_pushint64(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
