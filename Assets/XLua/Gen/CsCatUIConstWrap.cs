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
    public class CsCatUIConstWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CsCat.UIConst);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 3, 3);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "UICanvas_Path", _g_get_UICanvas_Path);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "UICamera_Path", _g_get_UICamera_Path);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "UIManager_Path", _g_get_UIManager_Path);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "UICanvas_Path", _s_set_UICanvas_Path);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "UICamera_Path", _s_set_UICamera_Path);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "UIManager_Path", _s_set_UIManager_Path);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "CsCat.UIConst does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UICanvas_Path(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.UIConst.UICanvas_Path);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UICamera_Path(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.UIConst.UICamera_Path);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UIManager_Path(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, CsCat.UIConst.UIManager_Path);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UICanvas_Path(RealStatePtr L)
        {
		    try {
                
			    CsCat.UIConst.UICanvas_Path = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UICamera_Path(RealStatePtr L)
        {
		    try {
                
			    CsCat.UIConst.UICamera_Path = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UIManager_Path(RealStatePtr L)
        {
		    try {
                
			    CsCat.UIConst.UIManager_Path = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
