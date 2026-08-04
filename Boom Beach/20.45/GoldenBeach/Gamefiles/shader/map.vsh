#ifdef GL_ES
#else
#define highp
#define mediump
#define lowp
#endif
uniform   mat4 u_mvp;

attribute vec4 a_pos;
attribute vec2 a_uv0;

varying highp vec2 v_texcoord0;
varying lowp float v_result;
varying highp vec2 v_normalCoord;

varying lowp vec3 v_waterLightSea;
varying lowp vec3 v_waterDarkSea;
varying lowp vec3 v_waterLightShore;
varying lowp vec3 v_waterDarkShore;

uniform highp float u_time;
uniform mediump float u_uvFactor;

varying float v_reflectionPower;

void main()
{
	gl_Position = u_mvp * a_pos;
	v_texcoord0 = a_uv0;

	float ystretch = 0.2;//0.4;
	v_reflectionPower = clamp((1.0 - length(vec2(a_pos.x * 0.7 + (a_uv0.x - 0.5) * 1.5, a_pos.y * ystretch) - vec2(0.0, ystretch))) * 3.0, 1.5, 4.0);
	
	
	float x = v_texcoord0.x;
	float y = v_texcoord0.y * u_uvFactor;
//	float mov0 = x + y + cos(sin(u_time) * 2.0) * 100.0 + sin(x / 100.0) * 1000.0;
	float mov1 = y / 0.04 * 5.0 + u_time;
	float mov2 = x / 0.02 * 5.0;
	float c1 = abs((sin(mov1 + u_time) + mov2) * 0.5 - mov1 - mov2 + u_time);
//	float c2 = abs(sin(c1 + sin(mov0 / 1000.0 + u_time) + sin(y / 40.0 + u_time) + sin((x + y) / 100.0) * 3.0));

	float c4 = 0.5 * sin(sqrt(x * x + y * y) * 150.0 - u_time) + 0.5;

//	float c5 = 0.5 * sin((x + cos(y + u_time * 0.01) + u_time * 0.01) * 100.0) + 0.5;

	c1 = 0.5 * sin(c1) + 0.5;
	v_result = c4;
	
	v_normalCoord = v_texcoord0 * 25.0;
	v_normalCoord.x -= 0.01 * u_time * 0.5;
	v_normalCoord.y += 0.02 * u_time * 0.5;

	v_normalCoord = vec2(v_normalCoord.x + c1 * 0.01, (v_normalCoord.y + c1 * 0.01) * u_uvFactor) * 1.5;
	
#ifdef SIMPLEi
	float v_dimmingFactor = 1.35 - sqrt((v_texcoord0.x - 0.5) * (v_texcoord0.x - 0.5) + (v_texcoord0.y - 0.5) * (v_texcoord0.y - 0.5));
	v_dimmingFactor *= v_dimmingFactor;
	v_dimmingFactor = min(v_dimmingFactor, 1.0);
    
    v_waterLightSea = vec3(float(0x2A) / 255.0, float(0x74) / 255.0, float(0xB8) / 255.0) * 1.3 * v_dimmingFactor;
    lowp vec3 v_waterLightShore = vec3(float(0xC0) / 255.0, float(0xF4) / 255.0, float(0xE3) / 255.0) * v_dimmingFactor;

    v_waterDarkSea = vec3(float(0x1D) / 255.0, float(0x36) / 255.0, float(0x5B) / 255.0) * 1.3 * v_dimmingFactor;
    lowp vec3 v_waterDarkShore = vec3(float(0x0B) / 255.0, float(0xAC) / 255.0, float(0x88) / 255.0) * v_dimmingFactor;

	float blendFactor = 0.2;
	v_waterDarkSea = mix(v_waterDarkSea, v_waterDarkShore, blendFactor);
	v_waterLightSea = mix(v_waterLightSea, v_waterLightShore, blendFactor);
#endif // SIMPLE
/*
	float blendForce = (a_pos.y * 0.5 + 0.1) * 0.3;
	vec3 tempColor = vec3(float(0x90) / 255.0, float(0xD3) / 255.0, float(0xFD) / 255.0);
	v_waterDarkSea = mix(v_waterDarkSea, tempColor * 1.1, blendForce);
	v_waterDarkShore = mix(v_waterDarkShore, tempColor * 0.9, blendForce);
*/
}
