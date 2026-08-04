#ifdef GL_ES
precision mediump float;
#else
#define highp
#define mediump
#define lowp
#endif

uniform sampler2D texture0;

varying   vec2 v_texcoord0;
varying   vec2 v_texcoord1;
varying   vec4 v_color;

void main()
{
	float a = 1.0 - length(v_texcoord1 - vec2(1.0, 1.0));
	gl_FragColor = vec4(1.0, 1.0, 1.0, texture2D(texture0, v_texcoord0).b * a) * v_color;
}
