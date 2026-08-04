#ifdef GL_ES
precision mediump float;
#else
#define highp
#define mediump
#define lowp
#endif

uniform sampler2D texture0;

varying vec2 v_texcoord0;
varying float v_z;

uniform vec4 u_waterColor;
uniform float u_alpha;

void main()
{
	gl_FragColor = texture2D(texture0, v_texcoord0);
	//gl_FragColor = vec4(mix(u_waterColor.rgb, texture2D(texture0, v_texcoord0).rgb, clamp(v_z, 0.0, 1.0)), u_alpha * clamp(v_z, 0.0, 1.0));
}
