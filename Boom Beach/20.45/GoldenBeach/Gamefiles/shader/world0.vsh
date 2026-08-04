#ifdef GL_ES
#else
#define highp
#define mediump
#define lowp
#endif
uniform   mat4 u_mvp;
//uniform   mat4 u_tempMat;

attribute vec4 a_pos;
attribute vec3 a_normal;
attribute vec2 a_uv0;

varying   vec2 v_texcoord0;
varying   vec2 v_texcoordMap;

uniform highp float u_time;
uniform mediump float u_z;
//uniform mediump float u_1DivLevelWidth;
//uniform mediump float u_1DivLevelHeight;
//uniform bool u_useShadowMap;

#ifdef MEDIUM
#define SIMPLE
#endif

void main()
{
#ifdef SIMPLE
    vec4 pos = a_pos;
    gl_Position = u_mvp * pos;
	v_texcoord0 = a_uv0;
    v_texcoordMap = a_normal.xy;
#else
#ifdef MEDIUM
    vec4 pos = a_pos;
    gl_Position = u_mvp * pos;
	v_texcoord0 = a_uv0;
    v_texcoordMap = a_normal.xy;
#else
/*
    vec4 pos = a_pos;
	if (pos.z < u_z) {
		highp float tx = clamp((cos((a_pos.x + u_time) * a_pos.y * 0.03 + u_time) + 1.0) * 0.5, 0.0, 1.0);
		highp float ty = clamp((cos((a_pos.x + u_time) * a_pos.y * 0.003 + u_time) + 1.0) * 0.5, 0.0, 1.0);
        pos.xy += vec2(tx, ty) * 5.0;
	}
    gl_Position = u_mvp * pos;
	v_normal = a_normal;
	v_texcoord0 = a_uv0;
	pos = u_tempMat * pos;
	v_texcoordMap = vec2(pos.x * u_1DivLevelWidth, pos.y * u_1DivLevelHeight);
    if (u_useShadowMap) {
        v_texcoordMap = v_normal.xy;
    }
	*/
#endif	// MEDIUM
#endif	// SIMPLE
}
