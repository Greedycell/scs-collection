#ifdef GL_ES
precision mediump float;
#else
#define highp
#define mediump
#define lowp
#endif

uniform sampler2D texture0;

varying vec2 v_texcoord0;
varying vec2 v_texcoordMap;

void main()
{
	gl_FragColor = texture2D(texture0, v_texcoord0);
	//gl_FragColor.a = gl_FragColor.a * 0.5;
	//vec4 color = texture2D(texture0, v_texcoord0);
	//gl_FragColor = vec4(0.0, 0.0, 0.0, color.a * 0.4);
}
