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
    public class CsCatCameraUtilWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CsCat.CameraUtil);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 10, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "GetRectSizeByDistance", _m_GetRectSizeByDistance_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetRectOfLocalByDistance", _m_GetRectOfLocalByDistance_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetRectOfWorldByDistance", _m_GetRectOfWorldByDistance_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WorldToUIPos", _m_WorldToUIPos_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ScreenToUIPos", _m_ScreenToUIPos_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ViewPortToUIPos", _m_ViewPortToUIPos_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "UIPosToWorldPos", _m_UIPosToWorldPos_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "UIPosToScreenPos", _m_UIPosToScreenPos_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "UIPosToViewPortPos", _m_UIPosToViewPortPos_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					CsCat.CameraUtil gen_ret = new CsCat.CameraUtil();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.CameraUtil constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRectSizeByDistance_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 1, typeof(UnityEngine.Camera));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        UnityEngine.Vector2 gen_ret = CsCat.CameraUtil.GetRectSizeByDistance( _camera, _distance );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRectOfLocalByDistance_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Camera>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 1, typeof(UnityEngine.Camera));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        CsCat.Rectangle3d gen_ret = CsCat.CameraUtil.GetRectOfLocalByDistance( _camera, _distance );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Camera>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector2>(L, 3)) 
                {
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 1, typeof(UnityEngine.Camera));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector2 _off_percent;translator.Get(L, 3, out _off_percent);
                    
                        CsCat.Rectangle3d gen_ret = CsCat.CameraUtil.GetRectOfLocalByDistance( _camera, _distance, _off_percent );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.CameraUtil.GetRectOfLocalByDistance!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRectOfWorldByDistance_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Camera>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 1, typeof(UnityEngine.Camera));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        CsCat.Rectangle3d gen_ret = CsCat.CameraUtil.GetRectOfWorldByDistance( _camera, _distance );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Camera>(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector2>(L, 3)) 
                {
                    UnityEngine.Camera _camera = (UnityEngine.Camera)translator.GetObject(L, 1, typeof(UnityEngine.Camera));
                    float _distance = (float)LuaAPI.lua_tonumber(L, 2);
                    UnityEngine.Vector2 _off_percent;translator.Get(L, 3, out _off_percent);
                    
                        CsCat.Rectangle3d gen_ret = CsCat.CameraUtil.GetRectOfWorldByDistance( _camera, _distance, _off_percent );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.CameraUtil.GetRectOfWorldByDistance!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WorldToUIPos_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Camera>(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& translator.Assignable<System.Nullable<UnityEngine.Vector2>>(L, 4)&& translator.Assignable<UnityEngine.Vector2>(L, 5)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Camera _world_camera = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));
                    UnityEngine.Vector3 _worldPosition;translator.Get(L, 3, out _worldPosition);
                    System.Nullable<UnityEngine.Vector2> _uiPosPivot;translator.Get(L, 4, out _uiPosPivot);
                    UnityEngine.Vector2 _offset;translator.Get(L, 5, out _offset);
                    
                        UnityEngine.Vector2 gen_ret = CsCat.CameraUtil.WorldToUIPos( _canvas_rectTransform, _world_camera, _worldPosition, _uiPosPivot, _offset );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Camera>(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& translator.Assignable<System.Nullable<UnityEngine.Vector2>>(L, 4)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Camera _world_camera = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));
                    UnityEngine.Vector3 _worldPosition;translator.Get(L, 3, out _worldPosition);
                    System.Nullable<UnityEngine.Vector2> _uiPosPivot;translator.Get(L, 4, out _uiPosPivot);
                    
                        UnityEngine.Vector2 gen_ret = CsCat.CameraUtil.WorldToUIPos( _canvas_rectTransform, _world_camera, _worldPosition, _uiPosPivot );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Camera>(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Camera _world_camera = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));
                    UnityEngine.Vector3 _worldPosition;translator.Get(L, 3, out _worldPosition);
                    
                        UnityEngine.Vector2 gen_ret = CsCat.CameraUtil.WorldToUIPos( _canvas_rectTransform, _world_camera, _worldPosition );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.CameraUtil.WorldToUIPos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ScreenToUIPos_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Camera>(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& translator.Assignable<System.Nullable<UnityEngine.Vector2>>(L, 4)&& translator.Assignable<UnityEngine.Vector2>(L, 5)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Camera _screen_camera = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));
                    UnityEngine.Vector3 _screenPoint;translator.Get(L, 3, out _screenPoint);
                    System.Nullable<UnityEngine.Vector2> _uiPosPivot;translator.Get(L, 4, out _uiPosPivot);
                    UnityEngine.Vector2 _offset;translator.Get(L, 5, out _offset);
                    
                        UnityEngine.Vector2 gen_ret = CsCat.CameraUtil.ScreenToUIPos( _canvas_rectTransform, _screen_camera, _screenPoint, _uiPosPivot, _offset );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Camera>(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& translator.Assignable<System.Nullable<UnityEngine.Vector2>>(L, 4)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Camera _screen_camera = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));
                    UnityEngine.Vector3 _screenPoint;translator.Get(L, 3, out _screenPoint);
                    System.Nullable<UnityEngine.Vector2> _uiPosPivot;translator.Get(L, 4, out _uiPosPivot);
                    
                        UnityEngine.Vector2 gen_ret = CsCat.CameraUtil.ScreenToUIPos( _canvas_rectTransform, _screen_camera, _screenPoint, _uiPosPivot );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Camera>(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Camera _screen_camera = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));
                    UnityEngine.Vector3 _screenPoint;translator.Get(L, 3, out _screenPoint);
                    
                        UnityEngine.Vector2 gen_ret = CsCat.CameraUtil.ScreenToUIPos( _canvas_rectTransform, _screen_camera, _screenPoint );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.CameraUtil.ScreenToUIPos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ViewPortToUIPos_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& translator.Assignable<System.Nullable<UnityEngine.Vector2>>(L, 3)&& translator.Assignable<UnityEngine.Vector2>(L, 4)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Vector3 _viewport_pos;translator.Get(L, 2, out _viewport_pos);
                    System.Nullable<UnityEngine.Vector2> _uiPosPivot;translator.Get(L, 3, out _uiPosPivot);
                    UnityEngine.Vector2 _offset;translator.Get(L, 4, out _offset);
                    
                        UnityEngine.Vector2 gen_ret = CsCat.CameraUtil.ViewPortToUIPos( _canvas_rectTransform, _viewport_pos, _uiPosPivot, _offset );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& translator.Assignable<System.Nullable<UnityEngine.Vector2>>(L, 3)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Vector3 _viewport_pos;translator.Get(L, 2, out _viewport_pos);
                    System.Nullable<UnityEngine.Vector2> _uiPosPivot;translator.Get(L, 3, out _uiPosPivot);
                    
                        UnityEngine.Vector2 gen_ret = CsCat.CameraUtil.ViewPortToUIPos( _canvas_rectTransform, _viewport_pos, _uiPosPivot );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Vector3>(L, 2)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Vector3 _viewport_pos;translator.Get(L, 2, out _viewport_pos);
                    
                        UnityEngine.Vector2 gen_ret = CsCat.CameraUtil.ViewPortToUIPos( _canvas_rectTransform, _viewport_pos );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.CameraUtil.ViewPortToUIPos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UIPosToWorldPos_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Camera>(L, 2)&& translator.Assignable<UnityEngine.Vector2>(L, 3)&& translator.Assignable<System.Nullable<UnityEngine.Vector2>>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& translator.Assignable<UnityEngine.Vector2>(L, 6)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Camera _world_camera = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));
                    UnityEngine.Vector2 _ui_pos;translator.Get(L, 3, out _ui_pos);
                    System.Nullable<UnityEngine.Vector2> _uiPosPivot;translator.Get(L, 4, out _uiPosPivot);
                    float _viewprot_z = (float)LuaAPI.lua_tonumber(L, 5);
                    UnityEngine.Vector2 _viewprot_offset;translator.Get(L, 6, out _viewprot_offset);
                    
                        UnityEngine.Vector2 gen_ret = CsCat.CameraUtil.UIPosToWorldPos( _canvas_rectTransform, _world_camera, _ui_pos, _uiPosPivot, _viewprot_z, _viewprot_offset );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Camera>(L, 2)&& translator.Assignable<UnityEngine.Vector2>(L, 3)&& translator.Assignable<System.Nullable<UnityEngine.Vector2>>(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Camera _world_camera = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));
                    UnityEngine.Vector2 _ui_pos;translator.Get(L, 3, out _ui_pos);
                    System.Nullable<UnityEngine.Vector2> _uiPosPivot;translator.Get(L, 4, out _uiPosPivot);
                    float _viewprot_z = (float)LuaAPI.lua_tonumber(L, 5);
                    
                        UnityEngine.Vector2 gen_ret = CsCat.CameraUtil.UIPosToWorldPos( _canvas_rectTransform, _world_camera, _ui_pos, _uiPosPivot, _viewprot_z );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Camera>(L, 2)&& translator.Assignable<UnityEngine.Vector2>(L, 3)&& translator.Assignable<System.Nullable<UnityEngine.Vector2>>(L, 4)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Camera _world_camera = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));
                    UnityEngine.Vector2 _ui_pos;translator.Get(L, 3, out _ui_pos);
                    System.Nullable<UnityEngine.Vector2> _uiPosPivot;translator.Get(L, 4, out _uiPosPivot);
                    
                        UnityEngine.Vector2 gen_ret = CsCat.CameraUtil.UIPosToWorldPos( _canvas_rectTransform, _world_camera, _ui_pos, _uiPosPivot );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Camera>(L, 2)&& translator.Assignable<UnityEngine.Vector2>(L, 3)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Camera _world_camera = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));
                    UnityEngine.Vector2 _ui_pos;translator.Get(L, 3, out _ui_pos);
                    
                        UnityEngine.Vector2 gen_ret = CsCat.CameraUtil.UIPosToWorldPos( _canvas_rectTransform, _world_camera, _ui_pos );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.CameraUtil.UIPosToWorldPos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UIPosToScreenPos_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Camera>(L, 2)&& translator.Assignable<UnityEngine.Vector2>(L, 3)&& translator.Assignable<System.Nullable<UnityEngine.Vector2>>(L, 4)&& translator.Assignable<UnityEngine.Vector2>(L, 5)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Camera _screen_camera = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));
                    UnityEngine.Vector2 _ui_pos;translator.Get(L, 3, out _ui_pos);
                    System.Nullable<UnityEngine.Vector2> _uiPosPivot;translator.Get(L, 4, out _uiPosPivot);
                    UnityEngine.Vector2 _viewprot_offset;translator.Get(L, 5, out _viewprot_offset);
                    
                        UnityEngine.Vector2 gen_ret = CsCat.CameraUtil.UIPosToScreenPos( _canvas_rectTransform, _screen_camera, _ui_pos, _uiPosPivot, _viewprot_offset );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Camera>(L, 2)&& translator.Assignable<UnityEngine.Vector2>(L, 3)&& translator.Assignable<System.Nullable<UnityEngine.Vector2>>(L, 4)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Camera _screen_camera = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));
                    UnityEngine.Vector2 _ui_pos;translator.Get(L, 3, out _ui_pos);
                    System.Nullable<UnityEngine.Vector2> _uiPosPivot;translator.Get(L, 4, out _uiPosPivot);
                    
                        UnityEngine.Vector2 gen_ret = CsCat.CameraUtil.UIPosToScreenPos( _canvas_rectTransform, _screen_camera, _ui_pos, _uiPosPivot );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Camera>(L, 2)&& translator.Assignable<UnityEngine.Vector2>(L, 3)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Camera _screen_camera = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));
                    UnityEngine.Vector2 _ui_pos;translator.Get(L, 3, out _ui_pos);
                    
                        UnityEngine.Vector2 gen_ret = CsCat.CameraUtil.UIPosToScreenPos( _canvas_rectTransform, _screen_camera, _ui_pos );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.CameraUtil.UIPosToScreenPos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UIPosToViewPortPos_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& translator.Assignable<System.Nullable<UnityEngine.Vector2>>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<UnityEngine.Vector2>(L, 5)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Vector2 _ui_pos;translator.Get(L, 2, out _ui_pos);
                    System.Nullable<UnityEngine.Vector2> _uiPosPivot;translator.Get(L, 3, out _uiPosPivot);
                    float _viewprot_z = (float)LuaAPI.lua_tonumber(L, 4);
                    UnityEngine.Vector2 _viewprot_offset;translator.Get(L, 5, out _viewprot_offset);
                    
                        UnityEngine.Vector3 gen_ret = CsCat.CameraUtil.UIPosToViewPortPos( _canvas_rectTransform, _ui_pos, _uiPosPivot, _viewprot_z, _viewprot_offset );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& translator.Assignable<System.Nullable<UnityEngine.Vector2>>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Vector2 _ui_pos;translator.Get(L, 2, out _ui_pos);
                    System.Nullable<UnityEngine.Vector2> _uiPosPivot;translator.Get(L, 3, out _uiPosPivot);
                    float _viewprot_z = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        UnityEngine.Vector3 gen_ret = CsCat.CameraUtil.UIPosToViewPortPos( _canvas_rectTransform, _ui_pos, _uiPosPivot, _viewprot_z );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Vector2>(L, 2)&& translator.Assignable<System.Nullable<UnityEngine.Vector2>>(L, 3)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Vector2 _ui_pos;translator.Get(L, 2, out _ui_pos);
                    System.Nullable<UnityEngine.Vector2> _uiPosPivot;translator.Get(L, 3, out _uiPosPivot);
                    
                        UnityEngine.Vector3 gen_ret = CsCat.CameraUtil.UIPosToViewPortPos( _canvas_rectTransform, _ui_pos, _uiPosPivot );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.RectTransform>(L, 1)&& translator.Assignable<UnityEngine.Vector2>(L, 2)) 
                {
                    UnityEngine.RectTransform _canvas_rectTransform = (UnityEngine.RectTransform)translator.GetObject(L, 1, typeof(UnityEngine.RectTransform));
                    UnityEngine.Vector2 _ui_pos;translator.Get(L, 2, out _ui_pos);
                    
                        UnityEngine.Vector3 gen_ret = CsCat.CameraUtil.UIPosToViewPortPos( _canvas_rectTransform, _ui_pos );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.CameraUtil.UIPosToViewPortPos!");
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
