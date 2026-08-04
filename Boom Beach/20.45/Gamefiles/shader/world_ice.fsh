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

void main()
{
    gl_FragColor = texture2D(texture1, v_texcoordMap);
}
