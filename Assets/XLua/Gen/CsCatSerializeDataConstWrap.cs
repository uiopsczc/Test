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
    public class CsCatSerializeDataConstWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CsCat.SerializeDataConst);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 3, 3);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Save_File_Path_cs", _g_get_Save_File_Path_cs);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Save_File_Path_cs2", _g_get_Save_File_Path_cs2);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Save_File_Path_lua", _g_get_Save_File_Path_lua);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Save_File_Path_cs", _s_set_Save_File_Path_cs);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Save_File_Path_cs2", _s_set_Save_File_Path_cs2);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Save_File_Path_lua", _s_set_Save_File_Path_lua);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					CsCat.SerializeDataConst gen_ret = new CsCat.SerializeDataConst();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.SerializeDataConst constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Save_File_Path_cs(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.SerializeDataConst.Save_File_Path_cs);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Save_File_Path_cs2(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.SerializeDataConst.Save_File_Path_cs2);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Save_File_Path_lua(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.SerializeDataConst.Save_File_Path_lua);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Save_File_Path_cs(RealStatePtr L)
        {
		    try {
                
			    CsCat.SerializeDataConst.Save_File_Path_cs = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Save_File_Path_cs2(RealStatePtr L)
        {
		    try {
                
			    CsCat.SerializeDataConst.Save_File_Path_cs2 = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Save_File_Path_lua(RealStatePtr L)
        {
		    try {
                
			    CsCat.SerializeDataConst.Save_File_Path_lua = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
