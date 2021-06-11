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
    public class UnityEngineGUIStyleWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityEngine.GUIStyle);
			Utils.BeginObjectRegister(type, L, translator, 0, 10, 28, 26);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Draw", _m_Draw);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DrawCursor", _m_DrawCursor);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DrawWithTextSelection", _m_DrawWithTextSelection);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCursorPixelPosition", _m_GetCursorPixelPosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCursorStringIndex", _m_GetCursorStringIndex);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CalcSize", _m_CalcSize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CalcScreenSize", _m_CalcScreenSize);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CalcHeight", _m_CalcHeight);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CalcMinMaxWidth", _m_CalcMinMaxWidth);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToString", _m_ToString);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "font", _g_get_font);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "imagePosition", _g_get_imagePosition);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "alignment", _g_get_alignment);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "wordWrap", _g_get_wordWrap);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "clipping", _g_get_clipping);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "contentOffset", _g_get_contentOffset);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fixedWidth", _g_get_fixedWidth);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fixedHeight", _g_get_fixedHeight);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "stretchWidth", _g_get_stretchWidth);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "stretchHeight", _g_get_stretchHeight);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fontSize", _g_get_fontSize);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fontStyle", _g_get_fontStyle);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "richText", _g_get_richText);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "name", _g_get_name);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "normal", _g_get_normal);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "hover", _g_get_hover);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "active", _g_get_active);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onNormal", _g_get_onNormal);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onHover", _g_get_onHover);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onActive", _g_get_onActive);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "focused", _g_get_focused);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onFocused", _g_get_onFocused);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "border", _g_get_border);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "margin", _g_get_margin);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "padding", _g_get_padding);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "overflow", _g_get_overflow);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "lineHeight", _g_get_lineHeight);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isHeightDependantOnWidth", _g_get_isHeightDependantOnWidth);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "font", _s_set_font);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "imagePosition", _s_set_imagePosition);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "alignment", _s_set_alignment);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "wordWrap", _s_set_wordWrap);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "clipping", _s_set_clipping);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "contentOffset", _s_set_contentOffset);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "fixedWidth", _s_set_fixedWidth);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "fixedHeight", _s_set_fixedHeight);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "stretchWidth", _s_set_stretchWidth);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "stretchHeight", _s_set_stretchHeight);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "fontSize", _s_set_fontSize);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "fontStyle", _s_set_fontStyle);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "richText", _s_set_richText);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "name", _s_set_name);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "normal", _s_set_normal);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "hover", _s_set_hover);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "active", _s_set_active);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onNormal", _s_set_onNormal);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onHover", _s_set_onHover);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onActive", _s_set_onActive);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "focused", _s_set_focused);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onFocused", _s_set_onFocused);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "border", _s_set_border);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "margin", _s_set_margin);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "padding", _s_set_padding);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "overflow", _s_set_overflow);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 1, 0);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "none", _g_get_none);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					UnityEngine.GUIStyle gen_ret = new UnityEngine.GUIStyle();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<UnityEngine.GUIStyle>(L, 2))
				{
					UnityEngine.GUIStyle _other = (UnityEngine.GUIStyle)translator.GetObject(L, 2, typeof(UnityEngine.GUIStyle));
					
					UnityEngine.GUIStyle gen_ret = new UnityEngine.GUIStyle(_other);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.GUIStyle constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Draw(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.Rect>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    UnityEngine.Rect _position;translator.Get(L, 2, out _position);
                    bool _isHover = LuaAPI.lua_toboolean(L, 3);
                    bool _isActive = LuaAPI.lua_toboolean(L, 4);
                    bool _on = LuaAPI.lua_toboolean(L, 5);
                    bool _hasKeyboardFocus = LuaAPI.lua_toboolean(L, 6);
                    
                    gen_to_be_invoked.Draw( _position, _isHover, _isActive, _on, _hasKeyboardFocus );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Rect>(L, 2)&& translator.Assignable<UnityEngine.GUIContent>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Rect _position;translator.Get(L, 2, out _position);
                    UnityEngine.GUIContent _content = (UnityEngine.GUIContent)translator.GetObject(L, 3, typeof(UnityEngine.GUIContent));
                    int _controlID = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.Draw( _position, _content, _controlID );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Rect>(L, 2)&& translator.Assignable<UnityEngine.GUIContent>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.Rect _position;translator.Get(L, 2, out _position);
                    UnityEngine.GUIContent _content = (UnityEngine.GUIContent)translator.GetObject(L, 3, typeof(UnityEngine.GUIContent));
                    int _controlID = LuaAPI.xlua_tointeger(L, 4);
                    bool _on = LuaAPI.lua_toboolean(L, 5);
                    
                    gen_to_be_invoked.Draw( _position, _content, _controlID, _on );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 6&& translator.Assignable<UnityEngine.Rect>(L, 2)&& translator.Assignable<UnityEngine.GUIContent>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    UnityEngine.Rect _position;translator.Get(L, 2, out _position);
                    UnityEngine.GUIContent _content = (UnityEngine.GUIContent)translator.GetObject(L, 3, typeof(UnityEngine.GUIContent));
                    int _controlID = LuaAPI.xlua_tointeger(L, 4);
                    bool _on = LuaAPI.lua_toboolean(L, 5);
                    bool _hover = LuaAPI.lua_toboolean(L, 6);
                    
                    gen_to_be_invoked.Draw( _position, _content, _controlID, _on, _hover );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 7&& translator.Assignable<UnityEngine.Rect>(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)) 
                {
                    UnityEngine.Rect _position;translator.Get(L, 2, out _position);
                    string _text = LuaAPI.lua_tostring(L, 3);
                    bool _isHover = LuaAPI.lua_toboolean(L, 4);
                    bool _isActive = LuaAPI.lua_toboolean(L, 5);
                    bool _on = LuaAPI.lua_toboolean(L, 6);
                    bool _hasKeyboardFocus = LuaAPI.lua_toboolean(L, 7);
                    
                    gen_to_be_invoked.Draw( _position, _text, _isHover, _isActive, _on, _hasKeyboardFocus );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 7&& translator.Assignable<UnityEngine.Rect>(L, 2)&& translator.Assignable<UnityEngine.Texture>(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)) 
                {
                    UnityEngine.Rect _position;translator.Get(L, 2, out _position);
                    UnityEngine.Texture _image = (UnityEngine.Texture)translator.GetObject(L, 3, typeof(UnityEngine.Texture));
                    bool _isHover = LuaAPI.lua_toboolean(L, 4);
                    bool _isActive = LuaAPI.lua_toboolean(L, 5);
                    bool _on = LuaAPI.lua_toboolean(L, 6);
                    bool _hasKeyboardFocus = LuaAPI.lua_toboolean(L, 7);
                    
                    gen_to_be_invoked.Draw( _position, _image, _isHover, _isActive, _on, _hasKeyboardFocus );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 7&& translator.Assignable<UnityEngine.Rect>(L, 2)&& translator.Assignable<UnityEngine.GUIContent>(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7)) 
                {
                    UnityEngine.Rect _position;translator.Get(L, 2, out _position);
                    UnityEngine.GUIContent _content = (UnityEngine.GUIContent)translator.GetObject(L, 3, typeof(UnityEngine.GUIContent));
                    bool _isHover = LuaAPI.lua_toboolean(L, 4);
                    bool _isActive = LuaAPI.lua_toboolean(L, 5);
                    bool _on = LuaAPI.lua_toboolean(L, 6);
                    bool _hasKeyboardFocus = LuaAPI.lua_toboolean(L, 7);
                    
                    gen_to_be_invoked.Draw( _position, _content, _isHover, _isActive, _on, _hasKeyboardFocus );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.GUIStyle.Draw!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DrawCursor(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Rect _position;translator.Get(L, 2, out _position);
                    UnityEngine.GUIContent _content = (UnityEngine.GUIContent)translator.GetObject(L, 3, typeof(UnityEngine.GUIContent));
                    int _controlID = LuaAPI.xlua_tointeger(L, 4);
                    int _character = LuaAPI.xlua_tointeger(L, 5);
                    
                    gen_to_be_invoked.DrawCursor( _position, _content, _controlID, _character );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DrawWithTextSelection(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Rect _position;translator.Get(L, 2, out _position);
                    UnityEngine.GUIContent _content = (UnityEngine.GUIContent)translator.GetObject(L, 3, typeof(UnityEngine.GUIContent));
                    int _controlID = LuaAPI.xlua_tointeger(L, 4);
                    int _firstSelectedCharacter = LuaAPI.xlua_tointeger(L, 5);
                    int _lastSelectedCharacter = LuaAPI.xlua_tointeger(L, 6);
                    
                    gen_to_be_invoked.DrawWithTextSelection( _position, _content, _controlID, _firstSelectedCharacter, _lastSelectedCharacter );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCursorPixelPosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Rect _position;translator.Get(L, 2, out _position);
                    UnityEngine.GUIContent _content = (UnityEngine.GUIContent)translator.GetObject(L, 3, typeof(UnityEngine.GUIContent));
                    int _cursorStringIndex = LuaAPI.xlua_tointeger(L, 4);
                    
                        UnityEngine.Vector2 gen_ret = gen_to_be_invoked.GetCursorPixelPosition( _position, _content, _cursorStringIndex );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCursorStringIndex(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Rect _position;translator.Get(L, 2, out _position);
                    UnityEngine.GUIContent _content = (UnityEngine.GUIContent)translator.GetObject(L, 3, typeof(UnityEngine.GUIContent));
                    UnityEngine.Vector2 _cursorPixelPosition;translator.Get(L, 4, out _cursorPixelPosition);
                    
                        int gen_ret = gen_to_be_invoked.GetCursorStringIndex( _position, _content, _cursorPixelPosition );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CalcSize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GUIContent _content = (UnityEngine.GUIContent)translator.GetObject(L, 2, typeof(UnityEngine.GUIContent));
                    
                        UnityEngine.Vector2 gen_ret = gen_to_be_invoked.CalcSize( _content );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CalcScreenSize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector2 _contentSize;translator.Get(L, 2, out _contentSize);
                    
                        UnityEngine.Vector2 gen_ret = gen_to_be_invoked.CalcScreenSize( _contentSize );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CalcHeight(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GUIContent _content = (UnityEngine.GUIContent)translator.GetObject(L, 2, typeof(UnityEngine.GUIContent));
                    float _width = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        float gen_ret = gen_to_be_invoked.CalcHeight( _content, _width );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CalcMinMaxWidth(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GUIContent _content = (UnityEngine.GUIContent)translator.GetObject(L, 2, typeof(UnityEngine.GUIContent));
                    float _minWidth;
                    float _maxWidth;
                    
                    gen_to_be_invoked.CalcMinMaxWidth( _content, out _minWidth, out _maxWidth );
                    LuaAPI.lua_pushnumber(L, _minWidth);
                        
                    LuaAPI.lua_pushnumber(L, _maxWidth);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToString(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.ToString(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_font(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.font);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_imagePosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.imagePosition);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_alignment(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineTextAnchor(L, gen_to_be_invoked.alignment);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_wordWrap(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.wordWrap);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_clipping(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.clipping);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_contentOffset(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, gen_to_be_invoked.contentOffset);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fixedWidth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.fixedWidth);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fixedHeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.fixedHeight);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_stretchWidth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.stretchWidth);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_stretchHeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.stretchHeight);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fontSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.fontSize);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fontStyle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.fontStyle);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_richText(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.richText);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_name(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.name);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_normal(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.normal);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_hover(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.hover);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_active(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.active);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onNormal(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onNormal);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onHover(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onHover);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onActive(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onActive);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_focused(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.focused);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onFocused(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onFocused);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_border(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.border);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_margin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.margin);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_padding(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.padding);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_overflow(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.overflow);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_lineHeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.lineHeight);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_none(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, UnityEngine.GUIStyle.none);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isHeightDependantOnWidth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isHeightDependantOnWidth);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_font(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.font = (UnityEngine.Font)translator.GetObject(L, 2, typeof(UnityEngine.Font));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_imagePosition(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                UnityEngine.ImagePosition gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.imagePosition = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_alignment(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                UnityEngine.TextAnchor gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.alignment = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_wordWrap(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.wordWrap = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_clipping(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                UnityEngine.TextClipping gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.clipping = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_contentOffset(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.contentOffset = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_fixedWidth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.fixedWidth = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_fixedHeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.fixedHeight = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_stretchWidth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.stretchWidth = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_stretchHeight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.stretchHeight = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_fontSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.fontSize = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_fontStyle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                UnityEngine.FontStyle gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.fontStyle = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_richText(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.richText = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_name(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.name = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_normal(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.normal = (UnityEngine.GUIStyleState)translator.GetObject(L, 2, typeof(UnityEngine.GUIStyleState));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_hover(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.hover = (UnityEngine.GUIStyleState)translator.GetObject(L, 2, typeof(UnityEngine.GUIStyleState));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_active(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.active = (UnityEngine.GUIStyleState)translator.GetObject(L, 2, typeof(UnityEngine.GUIStyleState));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onNormal(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onNormal = (UnityEngine.GUIStyleState)translator.GetObject(L, 2, typeof(UnityEngine.GUIStyleState));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onHover(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onHover = (UnityEngine.GUIStyleState)translator.GetObject(L, 2, typeof(UnityEngine.GUIStyleState));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onActive(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onActive = (UnityEngine.GUIStyleState)translator.GetObject(L, 2, typeof(UnityEngine.GUIStyleState));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_focused(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.focused = (UnityEngine.GUIStyleState)translator.GetObject(L, 2, typeof(UnityEngine.GUIStyleState));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onFocused(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onFocused = (UnityEngine.GUIStyleState)translator.GetObject(L, 2, typeof(UnityEngine.GUIStyleState));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_border(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.border = (UnityEngine.RectOffset)translator.GetObject(L, 2, typeof(UnityEngine.RectOffset));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_margin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.margin = (UnityEngine.RectOffset)translator.GetObject(L, 2, typeof(UnityEngine.RectOffset));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_padding(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.padding = (UnityEngine.RectOffset)translator.GetObject(L, 2, typeof(UnityEngine.RectOffset));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_overflow(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.GUIStyle gen_to_be_invoked = (UnityEngine.GUIStyle)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.overflow = (UnityEngine.RectOffset)translator.GetObject(L, 2, typeof(UnityEngine.RectOffset));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
