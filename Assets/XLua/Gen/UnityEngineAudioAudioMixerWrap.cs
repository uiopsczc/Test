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
    public class UnityEngineAudioAudioMixerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityEngine.Audio.AudioMixer);
			Utils.BeginObjectRegister(type, L, translator, 0, 6, 2, 2);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FindSnapshot", _m_FindSnapshot);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FindMatchingGroups", _m_FindMatchingGroups);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TransitionToSnapshots", _m_TransitionToSnapshots);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetFloat", _m_SetFloat);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearFloat", _m_ClearFloat);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetFloat", _m_GetFloat);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "outputAudioMixerGroup", _g_get_outputAudioMixerGroup);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "updateMode", _g_get_updateMode);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "outputAudioMixerGroup", _s_set_outputAudioMixerGroup);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "updateMode", _s_set_updateMode);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "UnityEngine.Audio.AudioMixer does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindSnapshot(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Audio.AudioMixer gen_to_be_invoked = (UnityEngine.Audio.AudioMixer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        UnityEngine.Audio.AudioMixerSnapshot gen_ret = gen_to_be_invoked.FindSnapshot( _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindMatchingGroups(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Audio.AudioMixer gen_to_be_invoked = (UnityEngine.Audio.AudioMixer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _subPath = LuaAPI.lua_tostring(L, 2);
                    
                        UnityEngine.Audio.AudioMixerGroup[] gen_ret = gen_to_be_invoked.FindMatchingGroups( _subPath );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TransitionToSnapshots(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Audio.AudioMixer gen_to_be_invoked = (UnityEngine.Audio.AudioMixer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Audio.AudioMixerSnapshot[] _snapshots = (UnityEngine.Audio.AudioMixerSnapshot[])translator.GetObject(L, 2, typeof(UnityEngine.Audio.AudioMixerSnapshot[]));
                    float[] _weights = (float[])translator.GetObject(L, 3, typeof(float[]));
                    float _timeToReach = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.TransitionToSnapshots( _snapshots, _weights, _timeToReach );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetFloat(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Audio.AudioMixer gen_to_be_invoked = (UnityEngine.Audio.AudioMixer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    float _value = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        bool gen_ret = gen_to_be_invoked.SetFloat( _name, _value );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearFloat(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Audio.AudioMixer gen_to_be_invoked = (UnityEngine.Audio.AudioMixer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.ClearFloat( _name );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFloat(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Audio.AudioMixer gen_to_be_invoked = (UnityEngine.Audio.AudioMixer)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    float _value;
                    
                        bool gen_ret = gen_to_be_invoked.GetFloat( _name, out _value );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    LuaAPI.lua_pushnumber(L, _value);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_outputAudioMixerGroup(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Audio.AudioMixer gen_to_be_invoked = (UnityEngine.Audio.AudioMixer)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.outputAudioMixerGroup);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_updateMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Audio.AudioMixer gen_to_be_invoked = (UnityEngine.Audio.AudioMixer)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.updateMode);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_outputAudioMixerGroup(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Audio.AudioMixer gen_to_be_invoked = (UnityEngine.Audio.AudioMixer)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.outputAudioMixerGroup = (UnityEngine.Audio.AudioMixerGroup)translator.GetObject(L, 2, typeof(UnityEngine.Audio.AudioMixerGroup));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_updateMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Audio.AudioMixer gen_to_be_invoked = (UnityEngine.Audio.AudioMixer)translator.FastGetCSObj(L, 1);
                UnityEngine.Audio.AudioMixerUpdateMode gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.updateMode = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
