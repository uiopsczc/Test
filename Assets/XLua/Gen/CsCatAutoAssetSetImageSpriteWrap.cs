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
    public class CsCatAutoAssetSetImageSpriteWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CsCat.AutoAssetSetImageSprite);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 3, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Set", _m_Set_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetAsync", _m_SetAsync_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					CsCat.AutoAssetSetImageSprite gen_ret = new CsCat.AutoAssetSetImageSprite();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AutoAssetSetImageSprite constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Set_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.UI.Image>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 4)&& translator.Assignable<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 5)&& translator.Assignable<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 6)) 
                {
                    UnityEngine.UI.Image _image = (UnityEngine.UI.Image)translator.GetObject(L, 1, typeof(UnityEngine.UI.Image));
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    bool _is_set_native_size = LuaAPI.lua_toboolean(L, 3);
                    System.Action<UnityEngine.UI.Image, UnityEngine.Sprite> _on_load_success_callback = translator.GetDelegate<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 4);
                    System.Action<UnityEngine.UI.Image, UnityEngine.Sprite> _on_load_fail_callback = translator.GetDelegate<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 5);
                    System.Action<UnityEngine.UI.Image, UnityEngine.Sprite> _on_load_done_callback = translator.GetDelegate<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 6);
                    
                    CsCat.AutoAssetSetImageSprite.Set( _image, _asset_path, _is_set_native_size, _on_load_success_callback, _on_load_fail_callback, _on_load_done_callback );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.UI.Image>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 4)&& translator.Assignable<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 5)) 
                {
                    UnityEngine.UI.Image _image = (UnityEngine.UI.Image)translator.GetObject(L, 1, typeof(UnityEngine.UI.Image));
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    bool _is_set_native_size = LuaAPI.lua_toboolean(L, 3);
                    System.Action<UnityEngine.UI.Image, UnityEngine.Sprite> _on_load_success_callback = translator.GetDelegate<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 4);
                    System.Action<UnityEngine.UI.Image, UnityEngine.Sprite> _on_load_fail_callback = translator.GetDelegate<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 5);
                    
                    CsCat.AutoAssetSetImageSprite.Set( _image, _asset_path, _is_set_native_size, _on_load_success_callback, _on_load_fail_callback );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.UI.Image>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 4)) 
                {
                    UnityEngine.UI.Image _image = (UnityEngine.UI.Image)translator.GetObject(L, 1, typeof(UnityEngine.UI.Image));
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    bool _is_set_native_size = LuaAPI.lua_toboolean(L, 3);
                    System.Action<UnityEngine.UI.Image, UnityEngine.Sprite> _on_load_success_callback = translator.GetDelegate<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 4);
                    
                    CsCat.AutoAssetSetImageSprite.Set( _image, _asset_path, _is_set_native_size, _on_load_success_callback );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.UI.Image>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.UI.Image _image = (UnityEngine.UI.Image)translator.GetObject(L, 1, typeof(UnityEngine.UI.Image));
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    bool _is_set_native_size = LuaAPI.lua_toboolean(L, 3);
                    
                    CsCat.AutoAssetSetImageSprite.Set( _image, _asset_path, _is_set_native_size );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.UI.Image>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    UnityEngine.UI.Image _image = (UnityEngine.UI.Image)translator.GetObject(L, 1, typeof(UnityEngine.UI.Image));
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    
                    CsCat.AutoAssetSetImageSprite.Set( _image, _asset_path );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 7&& translator.Assignable<UnityEngine.UI.Image>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.Vector2>(L, 4)&& translator.Assignable<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 5)&& translator.Assignable<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 6)&& translator.Assignable<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 7)) 
                {
                    UnityEngine.UI.Image _image = (UnityEngine.UI.Image)translator.GetObject(L, 1, typeof(UnityEngine.UI.Image));
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    bool _is_set_native_size = LuaAPI.lua_toboolean(L, 3);
                    UnityEngine.Vector2 _new_size;translator.Get(L, 4, out _new_size);
                    System.Action<UnityEngine.UI.Image, UnityEngine.Sprite> _on_load_success_callback = translator.GetDelegate<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 5);
                    System.Action<UnityEngine.UI.Image, UnityEngine.Sprite> _on_load_fail_callback = translator.GetDelegate<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 6);
                    System.Action<UnityEngine.UI.Image, UnityEngine.Sprite> _on_load_done_callback = translator.GetDelegate<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 7);
                    
                    CsCat.AutoAssetSetImageSprite.Set( _image, _asset_path, _is_set_native_size, _new_size, _on_load_success_callback, _on_load_fail_callback, _on_load_done_callback );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.UI.Image>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.Vector2>(L, 4)&& translator.Assignable<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 5)&& translator.Assignable<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 6)) 
                {
                    UnityEngine.UI.Image _image = (UnityEngine.UI.Image)translator.GetObject(L, 1, typeof(UnityEngine.UI.Image));
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    bool _is_set_native_size = LuaAPI.lua_toboolean(L, 3);
                    UnityEngine.Vector2 _new_size;translator.Get(L, 4, out _new_size);
                    System.Action<UnityEngine.UI.Image, UnityEngine.Sprite> _on_load_success_callback = translator.GetDelegate<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 5);
                    System.Action<UnityEngine.UI.Image, UnityEngine.Sprite> _on_load_fail_callback = translator.GetDelegate<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 6);
                    
                    CsCat.AutoAssetSetImageSprite.Set( _image, _asset_path, _is_set_native_size, _new_size, _on_load_success_callback, _on_load_fail_callback );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.UI.Image>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.Vector2>(L, 4)&& translator.Assignable<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 5)) 
                {
                    UnityEngine.UI.Image _image = (UnityEngine.UI.Image)translator.GetObject(L, 1, typeof(UnityEngine.UI.Image));
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    bool _is_set_native_size = LuaAPI.lua_toboolean(L, 3);
                    UnityEngine.Vector2 _new_size;translator.Get(L, 4, out _new_size);
                    System.Action<UnityEngine.UI.Image, UnityEngine.Sprite> _on_load_success_callback = translator.GetDelegate<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 5);
                    
                    CsCat.AutoAssetSetImageSprite.Set( _image, _asset_path, _is_set_native_size, _new_size, _on_load_success_callback );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.UI.Image>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.Vector2>(L, 4)) 
                {
                    UnityEngine.UI.Image _image = (UnityEngine.UI.Image)translator.GetObject(L, 1, typeof(UnityEngine.UI.Image));
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    bool _is_set_native_size = LuaAPI.lua_toboolean(L, 3);
                    UnityEngine.Vector2 _new_size;translator.Get(L, 4, out _new_size);
                    
                    CsCat.AutoAssetSetImageSprite.Set( _image, _asset_path, _is_set_native_size, _new_size );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AutoAssetSetImageSprite.Set!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetAsync_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.UI.Image>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.UI.Image _image = (UnityEngine.UI.Image)translator.GetObject(L, 1, typeof(UnityEngine.UI.Image));
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    bool _is_set_native_size = LuaAPI.lua_toboolean(L, 3);
                    
                        System.Collections.IEnumerator gen_ret = CsCat.AutoAssetSetImageSprite.SetAsync( _image, _asset_path, _is_set_native_size );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.UI.Image>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    UnityEngine.UI.Image _image = (UnityEngine.UI.Image)translator.GetObject(L, 1, typeof(UnityEngine.UI.Image));
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    
                        System.Collections.IEnumerator gen_ret = CsCat.AutoAssetSetImageSprite.SetAsync( _image, _asset_path );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 7&& translator.Assignable<UnityEngine.UI.Image>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.Vector2>(L, 4)&& translator.Assignable<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 5)&& translator.Assignable<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 6)&& translator.Assignable<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 7)) 
                {
                    UnityEngine.UI.Image _image = (UnityEngine.UI.Image)translator.GetObject(L, 1, typeof(UnityEngine.UI.Image));
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    bool _is_set_native_size = LuaAPI.lua_toboolean(L, 3);
                    UnityEngine.Vector2 _new_size;translator.Get(L, 4, out _new_size);
                    System.Action<UnityEngine.UI.Image, UnityEngine.Sprite> _on_load_success_callback = translator.GetDelegate<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 5);
                    System.Action<UnityEngine.UI.Image, UnityEngine.Sprite> _on_load_fail_callback = translator.GetDelegate<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 6);
                    System.Action<UnityEngine.UI.Image, UnityEngine.Sprite> _on_load_done_callback = translator.GetDelegate<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 7);
                    
                        System.Collections.IEnumerator gen_ret = CsCat.AutoAssetSetImageSprite.SetAsync( _image, _asset_path, _is_set_native_size, _new_size, _on_load_success_callback, _on_load_fail_callback, _on_load_done_callback );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.UI.Image>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.Vector2>(L, 4)&& translator.Assignable<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 5)&& translator.Assignable<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 6)) 
                {
                    UnityEngine.UI.Image _image = (UnityEngine.UI.Image)translator.GetObject(L, 1, typeof(UnityEngine.UI.Image));
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    bool _is_set_native_size = LuaAPI.lua_toboolean(L, 3);
                    UnityEngine.Vector2 _new_size;translator.Get(L, 4, out _new_size);
                    System.Action<UnityEngine.UI.Image, UnityEngine.Sprite> _on_load_success_callback = translator.GetDelegate<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 5);
                    System.Action<UnityEngine.UI.Image, UnityEngine.Sprite> _on_load_fail_callback = translator.GetDelegate<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 6);
                    
                        System.Collections.IEnumerator gen_ret = CsCat.AutoAssetSetImageSprite.SetAsync( _image, _asset_path, _is_set_native_size, _new_size, _on_load_success_callback, _on_load_fail_callback );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.UI.Image>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.Vector2>(L, 4)&& translator.Assignable<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 5)) 
                {
                    UnityEngine.UI.Image _image = (UnityEngine.UI.Image)translator.GetObject(L, 1, typeof(UnityEngine.UI.Image));
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    bool _is_set_native_size = LuaAPI.lua_toboolean(L, 3);
                    UnityEngine.Vector2 _new_size;translator.Get(L, 4, out _new_size);
                    System.Action<UnityEngine.UI.Image, UnityEngine.Sprite> _on_load_success_callback = translator.GetDelegate<System.Action<UnityEngine.UI.Image, UnityEngine.Sprite>>(L, 5);
                    
                        System.Collections.IEnumerator gen_ret = CsCat.AutoAssetSetImageSprite.SetAsync( _image, _asset_path, _is_set_native_size, _new_size, _on_load_success_callback );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.UI.Image>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<UnityEngine.Vector2>(L, 4)) 
                {
                    UnityEngine.UI.Image _image = (UnityEngine.UI.Image)translator.GetObject(L, 1, typeof(UnityEngine.UI.Image));
                    string _asset_path = LuaAPI.lua_tostring(L, 2);
                    bool _is_set_native_size = LuaAPI.lua_toboolean(L, 3);
                    UnityEngine.Vector2 _new_size;translator.Get(L, 4, out _new_size);
                    
                        System.Collections.IEnumerator gen_ret = CsCat.AutoAssetSetImageSprite.SetAsync( _image, _asset_path, _is_set_native_size, _new_size );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to CsCat.AutoAssetSetImageSprite.SetAsync!");
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
