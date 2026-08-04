#ifdef GL_ES
precision highp float;
#else
#define highp
#define mediump
#define lowp
#endif

uniform sampler2D texture0;
uniform sampler2D normal;
uniform sampler2D foam;

varying vec3 v_normal;
varying vec2 v_texcoordFoam;
varying vec2 v_normalUv;
varying float v_blendWeight;

uniform highp float u_time;

uniform mediump vec3 u_darkColor;
uniform mediump vec3 u_lightColor;

void main()
{
	vec4 params = texture2D(texture0, v_normal.xy);
	vec4 normal2 = texture2D(normal, v_normalUv);
	vec3 foamPower = texture2D(foam, v_texcoordFoam).rgb * (params.b * 0.25);
	gl_FragColor = (vec4(mix(u_lightColor, u_darkColor, normal2.x * v_blendWeight + (normal2.y + (-v_blendWeight * normal2.y))), params.g) + vec4(foamPower, foamPower.r) * 2.0) * (vec4(params.r, params.r, params.r, 1.0) * 1.3);
//	gl_FragColor = (vec4(mix(u_lightColor, u_darkColor, normal2.x * v_blendWeight + (normal2.y * (1.0 - v_blendWeight))), params.g) + vec4(foamPower, foamPower.r) * 2.0) * (vec4(params.r, params.r, params.r, 1.0) * 1.3);
}
