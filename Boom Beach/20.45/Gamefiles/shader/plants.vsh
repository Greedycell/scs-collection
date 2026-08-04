#ifdef GL_ES
#else
#define highp
#define mediump
#define lowp
#endif
uniform   mat4 u_mvp;

attribute vec3 a_pos;
attribute vec3 a_normal;
attribute vec2 a_uv0;
attribute vec2 a_uv1;

varying   vec2 v_texcoord0;
varying   vec2 v_texcoordMap;

uniform highp float u_time;
uniform mediump float u_1DivLevelWidth;
uniform mediump float u_1DivLevelHeight;

void main()
{
	float mapPos = a_normal.x * 0.1 + a_normal.y * 0.6;
	vec2 v1 = vec2(cos(mapPos + u_time * 0.6), cos(mapPos + u_time * 0.9));
	mapPos = (a_normal.x + a_normal.y + a_pos.x + a_pos.y) * 3.0;
	vec2 v2 = vec2(cos(mapPos + u_time * 3.2 + (a_uv1.y * 0.1)), cos(mapPos + u_time * 2.9 + (a_uv1.y * 0.1))) * 0.02 * a_uv1.y;

	vec3 normal = normalize(vec3(v1 * (a_uv1.x * 0.2), 5.0));
	//vec3 normal = vec3(0.0, 0.0, 1.0);
	vec3 binormal = normalize(cross(normal, vec3(1.0, 0.0, 0.0)));
	vec3 tangent = normalize(cross(binormal, normal));
	
	vec4 pos = vec4((a_pos * mat3(tangent, binormal, normal)) + a_normal, 1.0) + vec4(v2 * (a_uv1.y * 0.2), 0.0, 0.0);

	gl_Position = u_mvp * pos;
	v_texcoord0 = a_uv0;
	v_texcoordMap = vec2(pos.x * u_1DivLevelWidth, pos.y * u_1DivLevelHeight);
}
