#ifdef GL_ES
precision mediump float;
#else
#define highp
#define mediump
#define lowp
#endif

uniform sampler2D texture0;

varying vec2 v_texcoord0;
varying vec4 v_color;

uniform   vec4 u_color;

void main()
{
	gl_FragColor = vec4(v_color.rgb, v_color.a * texture2D(texture0, v_texcoord0).a);
}
