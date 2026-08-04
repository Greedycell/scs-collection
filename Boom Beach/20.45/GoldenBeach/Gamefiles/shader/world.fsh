#ifdef GL_ES
precision mediump float;
#else
#define highp
#define mediump
#define lowp
#endif

#define BLEND

uniform sampler2D texture0;
uniform sampler2D texture1;
#ifdef BLEND
uniform sampler2D lightmap;
#endif // BLEND

varying   vec2 v_texcoord0;
varying   vec2 v_texcoordMap;

uniform vec4 u_factor;

void main()
{
#ifdef SIMPLE
    gl_FragColor = texture2D(texture1, v_texcoordMap) * u_factor;
#else

#ifdef BLEND0
    gl_FragColor = texture2D(texture1, v_texcoordMap) * texture2D(texture0, v_texcoord0).gggr * u_factor;
#endif // BLEND0
#ifdef BLEND1
	gl_FragColor = texture2D(texture1, v_texcoordMap) * texture2D(texture0, v_texcoord0).bbbr * u_factor;
#endif // BLEND0
#ifdef BLEND
	vec4 material = texture2D(texture0, v_texcoord0);
	float gray = mix(material.g, material.b, texture2D(lightmap, v_texcoordMap).b) * u_factor.r;
	gl_FragColor = texture2D(texture1, v_texcoordMap) * vec4(gray, gray, gray, 1.0);
#endif // BLEND

#endif
}
