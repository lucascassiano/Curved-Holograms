/*---------------------------------------
Author: Lucas Cassiano - cassiano@mit.edu
Curved Projection
Creation Date: August 24th, 2016
Version: 1.2
Last Update: August 25th, 2016

This controls the Curved Projection Shader
-----------------------------------------*/
#define PI      3.1415926535897932384626433832795

struct v2f
{
	float4 pos : POSITION;
	float2 uv : TEXCOORD0;
};

float _Depth;
float _Width;

float _A;
float _B;
float _C;
float _D;

//A, B, C, D parameters
float _Ax;
float _Ay;
float _Bx;
float _By;
float _Cx;
float _Cy;
float _Dx;
float _Dy;

//Main Parameters
float _InternalRadius;
float _ExternalRadius;
float _ExpantionAngle;

//Invertions
int _VerticalFlip;
int _HorizontalFlip;

//Distortion Intensity;
float _Intensity;

//Offsets
float _OffsetX;
float _OffsetY;
float _W;
float _H;

v2f vert(appdata_img v)
{
	v2f o;
	o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
	o.uv = v.texcoord.xy;
	return o;
}


inline float2 CurvedCorrection(float2 uv)
{
	//float angle = abs(uv.x - 0.5)*_Width / _A;
	//float x2 = tan(angle)*_Depth * sign(uv.x - 0.5) / _Width + 0.5;
	//float x2 = x2 * 2;
	//x2 = //lerp(uv.x, x2, _Intensity);

	//float y2 = uv.y;
	//y2 *= sqrt((x2 - 0.5)*(x2 - 0.5)*_Width*_Width + _Depth*_Depth) / _Depth;
	//y2 += 0.5;
	//float y2 = uv.y * sin(uv.y);
	//y2 = uv.y* sqrt((x2 - 0.5)*(x2 - 0.5)*_Width*_Width + _Depth*_Depth) / _Depth;

	//y2 = lerp(uv.y, y2, _Intensity_Y);
	uv.y = 1-uv.y;

	float x2 = uv.x ;// *(uv.x - 0.5);
	float expan = _ExpantionAngle*(uv.y)*(uv.x-0.5);
	x2 = x2 + expan;
	//float x2 = _C*uv.x ;
	//x2 = lerp(uv.x, x2, _Intensity);

	//float y2 = uv.y* sqrt((x2 - 0.5)*(x2 - 0.5)*_A*_A + _B*_B) / _C;
	//float y2 = uv.y;
	//Best Version of Y2
	float y2 = uv.y + (1-uv.y*_ExternalRadius)*_InternalRadius*sin(x2 - 0.5)*(x2 - 0.5) ;// -(uv.y)*_B*sin(uv.x - 0.5)*(uv.x - 0.5);
	//Adding ABCD
	//float y2 = uv.y;
	//Internal Curve
	//float r1 = _ExternalRadius * sin(PI*uv.x);
	//External Curve
	//float r2 = (1 - uv.y)*_InternalRadius;
	//float r2 = _InternalRadius * sin(PI*uv.x);
//	y2 = y2 + r1*r2 -_Ay;
	//y2 = y2 + _Ay*uv.x*(uv.y-1) - _By*(uv.x-1);
	//Changing Intensity


	x2 = lerp(uv.x, x2, _Intensity);
	y2 = lerp(uv.y, y2, _Intensity);

	if (_VerticalFlip == 1)
		y2 = 1 - y2; //Vertical invertion
	if (_HorizontalFlip == 1)
		x2 = 1 - x2; //Horizontal invertion

	y2 = y2*_H + _OffsetY;
	x2 = x2*_W + _OffsetX;
	//x2 = uv.x;
	return float2(x2, y2);
}

//REFERENCES
/* PROCESSING FISHEYE
int u, v, r2;
float f;
for (int vd = - lsize; vd < lsize; vd++) {
for (int ud = - lsize; ud < lsize; ud++) {
r2 = ud*ud + vd*vd;
if (r2 <= lsize2) {
f = mag + k * r2;
u = floor(ud/f) + x - offX;
v = floor(vd/f) + y - offY;
//v=y;
if (u >= 0 && u < imgOrg.width && v >=0 && v < imgOrg.height)
set(ud + x, vd + y, imgOrg.get(u, v));
else
set(ud + x, vd + y, borderViaLens);
}
}
}

*/

/*OPENGL FISHEYE
1: varying vec4 normal, light_dir, eye_vec, lookat;
2: const float PI =  3.14159265;
3:
4: void main()
5: {
6: 	vec4 ambient, diffuse, specular;
7: 	float NdotL, RdotV;
8:
9: 	normal = vec4(gl_NormalMatrix * gl_Normal, 0.0);
10: 	vec4 vVertex = gl_ModelViewMatrix * gl_Vertex;
11: 	light_dir = gl_LightSource[0].position - vVertex;
12: 	eye_vec = -vVertex;
13:
14: 	vec4 temp_pos = ftransform();
15:
16: 	float dist = length(eye_vec);
17: 	lookat = eye_vec - temp_pos;
18: 	vec4 dir = temp_pos - eye_vec;
19: 	vec4 center = normalize(-eye_vec);
20: 	vec4 proj = dot(temp_pos, normalize(-lookat)) * normalize(-lookat);
21:
22: 	vec4 c = temp_pos - proj;
23:
24: 	float magnitude = .01;//1-acos(dot(normalize(-eye_vec), normalize(temp_pos)));
25:
26: 	c = length(c) * magnitude * normalize(c);
27:
28: 	vec4 dir2 = normalize(c-lookat);
29:
30: 	dir2 = (dir2 * dist);
31:
32: 	gl_Position.xyz = dir2.xyz;
33: 	gl_Position.w = ftransform().w;
34:
35: }
*/
