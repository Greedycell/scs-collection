#ifdef GL_ES
precision highp float;
#else
#define highp
#define mediump
#define lowp
#endif

uniform sampler2D texture0;
uniform sampler2D foam;

varying vec3 v_normal;
varying vec2 v_texcoordFoam;

uniform highp float u_time;

uniform mediump vec3 u_lightColor;

void main()
{
	vec4 params = texture2D(texture0, v_normal.xy);
	vec3 foamPower = texture2D(foam, v_texcoordFoam).rgb * (params.b * 0.5);
	gl_FragColor = (vec4(u_lightColor, params.g) - vec4(foamPower * 0.3, -foamPower.r) * 2.0) * (vec4(params.r, params.r, params.r, 1.0) * 1.3);
}
