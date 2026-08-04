#ifdef GL_ES
precision mediump float;
#else
#define highp
#define mediump
#define lowp
#endif

uniform sampler2D texture0;
uniform lowp sampler2D foam;

varying vec2 v_texcoord0;
varying vec2 v_foamUv;

uniform float u_time;

void main()
{
	vec4 tex = texture2D(texture0, v_texcoord0);
	vec4 foamColor = vec4(0.9, 0.9, 0.9, (texture2D(foam, v_foamUv).b * tex.g) * 0.9);
	foamColor.a = pow(foamColor.a * 1.2, 3.0);
	vec4 shadowColor = vec4(0.0, 0.0, 0.0, tex.b);
	gl_FragColor = vec4(mix(foamColor.rgb, shadowColor.rgb, shadowColor.a / (shadowColor.a + foamColor.a + 0.0001)), foamColor.a + shadowColor.a);
	//gl_FragColor = vec4(0.0, 0.0, 0.0, (1.0 - texture2D(texture0, v_texcoord0).b) * 0.2) + texture2D(foam, );
}
