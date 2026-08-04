#ifdef GL_ES
#else
#define highp
#define mediump
#define lowp
#endif
uniform   mat4 u_mvp;

attribute vec4 a_pos;
attribute vec2 a_uv0;

varying   vec4 v_pos;

uniform   vec3 u_pos;
uniform mediump vec2 u_radius;

void main()
{
	v_pos = vec4(a_pos.xyz * u_radius.y + a_pos.xyz, 1.0);
	gl_Position = u_mvp * (v_pos + vec4(u_pos, 1.0));
}
