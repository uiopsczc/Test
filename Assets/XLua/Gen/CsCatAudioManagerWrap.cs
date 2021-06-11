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
    public class CsCatAudioManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CsCat.AudioManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 10, 1, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetGroupVolume", _m_SetGroupVolume);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetGroupDecibel", _m_SetGroupDecibel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAllGroupVolume", _m_SetAllGroupVolume);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetAudioListenerPosition", _m_SetAudioListenerPosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PlaySound", _m_PlaySound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PlayUISound", _m_PlayUISound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PlayBGMSound", _m_PlayBGMSound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PauseBGMSound", _m_PauseBGMSound);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StopBGMSound", _m_StopBGMSound);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "last_bgm_path", _g_get_last_bgm_path);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "last_bgm_path", _s_set_last_bgm_path);
            
			
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
					
					CsCat.AudioManager gen_ret = new CsCat.AudioManager();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AudioManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AudioManager gen_to_be_invoked = (CsCat.AudioManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Init(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetGroupVolume(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AudioManager gen_to_be_invoked = (CsCat.AudioManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _group_name = LuaAPI.lua_tostring(L, 2);
                    float _value = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.SetGroupVolume( _group_name, _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetGroupDecibel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AudioManager gen_to_be_invoked = (CsCat.AudioManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _group_name = LuaAPI.lua_tostring(L, 2);
                    float _decibel = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.SetGroupDecibel( _group_name, _decibel );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAllGroupVolume(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AudioManager gen_to_be_invoked = (CsCat.AudioManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _value = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.SetAllGroupVolume( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAudioListenerPosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AudioManager gen_to_be_invoked = (CsCat.AudioManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    
                    gen_to_be_invoked.SetAudioListenerPosition( _position );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlaySound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AudioManager gen_to_be_invoked = (CsCat.AudioManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 7&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.GameObject>(L, 4)&& translator.Assignable<System.Nullable<UnityEngine.Vector3>>(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)&& translator.Assignable<System.Nullable<float>>(L, 7)) 
                {
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    string _group_name = LuaAPI.lua_tostring(L, 3);
                    UnityEngine.GameObject _target = (UnityEngine.GameObject)translator.GetObject(L, 4, typeof(UnityEngine.GameObject));
                    System.Nullable<UnityEngine.Vector3> _target_localPosition;translator.Get(L, 5, out _target_localPosition);
                    bool _is_loop = LuaAPI.lua_toboolean(L, 6);
                    System.Nullable<float> _range;translator.Get(L, 7, out _range);
                    
                    gen_to_be_invoked.PlaySound( _asset_path, _group_name, _target, _target_localPosition, _is_loop, _range );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 6&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.GameObject>(L, 4)&& translator.Assignable<System.Nullable<UnityEngine.Vector3>>(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    string _group_name = LuaAPI.lua_tostring(L, 3);
                    UnityEngine.GameObject _target = (UnityEngine.GameObject)translator.GetObject(L, 4, typeof(UnityEngine.GameObject));
                    System.Nullable<UnityEngine.Vector3> _target_localPosition;translator.Get(L, 5, out _target_localPosition);
                    bool _is_loop = LuaAPI.lua_toboolean(L, 6);
                    
                    gen_to_be_invoked.PlaySound( _asset_path, _group_name, _target, _target_localPosition, _is_loop );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.GameObject>(L, 4)&& translator.Assignable<System.Nullable<UnityEngine.Vector3>>(L, 5)) 
                {
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    string _group_name = LuaAPI.lua_tostring(L, 3);
                    UnityEngine.GameObject _target = (UnityEngine.GameObject)translator.GetObject(L, 4, typeof(UnityEngine.GameObject));
                    System.Nullable<UnityEngine.Vector3> _target_localPosition;translator.Get(L, 5, out _target_localPosition);
                    
                    gen_to_be_invoked.PlaySound( _asset_path, _group_name, _target, _target_localPosition );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AudioManager.PlaySound!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayUISound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AudioManager gen_to_be_invoked = (CsCat.AudioManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _sound_path = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.PlayUISound( _sound_path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayBGMSound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AudioManager gen_to_be_invoked = (CsCat.AudioManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _sound_path = LuaAPI.lua_tostring(L, 2);
                    bool _is_loop = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.PlayBGMSound( _sound_path, _is_loop );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _sound_path = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.PlayBGMSound( _sound_path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AudioManager.PlayBGMSound!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PauseBGMSound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AudioManager gen_to_be_invoked = (CsCat.AudioManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _is_paused = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.PauseBGMSound( _is_paused );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.PauseBGMSound(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AudioManager.PauseBGMSound!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopBGMSound(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                CsCat.AudioManager gen_to_be_invoked = (CsCat.AudioManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.StopBGMSound(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_last_bgm_path(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AudioManager gen_to_be_invoked = (CsCat.AudioManager)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.last_bgm_path);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_last_bgm_path(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CsCat.AudioManager gen_to_be_invoked = (CsCat.AudioManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.last_bgm_path = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
