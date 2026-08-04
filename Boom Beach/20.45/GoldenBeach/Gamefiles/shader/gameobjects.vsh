#ifdef GL_ES
#else
#define highp
#define mediump
#define lowp
#endif

uniform highp mat4 u_mvp;

attribute highp vec3 a_pos;
attribute mediump vec3 a_normal;
attribute lowp	vec2 a_uv0;
attribute highp vec4 a_boneindex;
attribute highp vec4 a_boneweights;

uniform highp mat4 u_bonematrices[8];
uniform mediump float u_z;

varying lowp vec2 v_texcoord0;
varying lowp float v_z;

void main()
{
	mediump ivec4 boneIndex = ivec4(a_boneindex);
	mediump vec4 boneWeights = a_boneweights;

	vec4 pos = vec4(a_pos, 1.0);
	highp vec4 position = u_bonematrices[boneIndex.x] * pos * boneWeights.x;

	position += u_bonematrices[boneIndex.y] * pos * boneWeights.y;
	position += u_bonematrices[boneIndex.z] * pos * boneWeights.z;
	position += u_bonematrices[boneIndex.w] * pos * boneWeights.w;

	gl_Position = u_mvp * position;
	v_z = (position.z + u_z) * 0.2;

	v_texcoord0 = a_uv0;
}