#ifdef GL_ES
precision mediump float;
#else
#define highp
#define mediump
#define lowp
#endif

uniform sampler2D texture0;
uniform sampler2D texture1;

varying vec3 v_color;
varying vec2 v_texcoord0;
varying vec2 v_texcoord1;
varying vec2 v_texcoordMap;

void main()
{
	float a = texture2D(texture1, v_texcoordMap).a;
	vec4 tmp = mix(texture2D(texture0, v_texcoord0), texture2D(texture0, v_texcoord1), a);
	gl_FragColor = vec4(tmp.rgb * v_color, 1.0);
}
