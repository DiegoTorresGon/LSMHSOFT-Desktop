/* The LSMHSOFT computes gravimetric geoid by the least squares modified Hotine's formula.
 * This program is distributed under the terms of a special License published by the author.
 * Hopefully, this program will be useful without warranty. See the License for more details. 
 *
 * Author    : R. Alpay ABBAK
 * Address   : Konya Technical University, Geomatics Engineering Dept, Campus, Konya, TURKEY
 * E-mail    : raabbak@ktun.edu.tr
 *
 * Please cite the following paper for the program:
 * Abbak RA, Ellmann A, Ustun A (2021). A practical software package for computing gravimetric 
 * geoid by the least squares modified Hotine's formula, Earth Science Informatics, 15(1), 1-12.
 *
 * Compilation of the program on Linux:
 * g++ LSMHSOFT.cpp matris.cpp levelell.cpp -o LSMHSOFT
 *
 * Execution of the program with the sample data: 
 ************** Original:
 * ./LSMHSOFT -Ggo_cons.gfc -Adisturbance.xyz -Eelevation.xyz -R45.01/47/1.01/5 -I0.02/0.02
 ************** Correction:
 * ./LSMHSOFT -Ggo_cons.gfc -Ddisturbance.xyz -Eelevation.xyz -R45.01/47/1.01/5 -I0.02/0.02
 * Created : 01.03.2021             v1.0
 */
#include<math.h>				// Standard math library
#include<stdlib.h>				// Standard general utility library
#include<stdio.h>				// Standard input and output library
#include<unistd.h>				// Standard option library 
#include<string.h>				// Standard string library
#include"matris.h"				// User-defined matrix library
#include"levelell.h"				// User-defined Level ellipsoid library
#define G 6.672585e-11				// Gravitational constant [m3/kg.s2]
#define GM 0.3986005e+15			// Earth mass-gravity constant
#define omega 7292115.0e-11			// Angular velocity of the Earth's rotation
#define	a 6378137.0				// Semimajor axes of GRS80 ellipsoid
#define J2 108263.0e-8				// Dynamical form factor of the Earth (GRS80)
#define R 6371000.0				// Earth mean radius
#define pi 3.1415926535897932			// Constant pi
#define rho 57.29577951308232			// 180 degree/pi
#define mx 1000					// Maximum dimension of the data area 
#define Nmax 2000				// Maximum degree of series expansion 
#define OPTIONS "G:D:E:T:M:P:C:V:R:I:SH"	// Options of the software
void MOD_PAR(int M,int v,double C0,matris c,matris dc,matris Q,matris E,matris &Sn,matris &bn);
void LEGENDRE(matris &P,int N,double x);
void HELP();
int main(int argc, char *argv[])
{//** DEFINITIONS *********************************************************************************/
	FILE 	 *model=NULL;			// reference global geopotential model
	FILE   *disturb=NULL;			// disturbing data file 
	FILE *elevation=NULL;			// elevation data file
	FILE   *density=NULL;			// density data file
	char 	      option;			// temprorary option
    	char       line[256];  			// read a line from model file
    	const char *slash="/";			// discriminant symbol
	int 	      v=1;			// type (biased:1 unbiased:2 optimum:3)
	int 	      s=0;			// shows if all segments are printed
	int           M=195;			// maximum expansion of GGM used
       	int           n=0;			// array element of degree
    	int           m=0;			// array element of order
    	int           i=0;			// array element
    	int           j=0;			// array element
	int           x=0;			// array element
    	int           y=0;			// array element
    	int          in=0;			// array element of computation point
    	int          jn=0;			// array element of computation point
	int        Mmax=0;			// maximum expansion of GGM
	int	   imax=(Nmax+1)*(Nmax+2)/2;	// maximum array element for computation
	int        mmax=0;			// element of max expansion of GGM
	double 	   psi0=0.5/rho;		// spherical cap size in radian
	double       C0=0.5;			// variance of terrestrial gravity data
	double 	 MinPhi=45.01;			// minimum latitude of target area
	double 	 MaxPhi=47.00;			// maximum latitude of target area
	double 	 MinLam=01.01;			// minimum longitude of target area
	double 	 MaxLam=05.00;			// maximum longitude of target area
	double 	 PhiInt=0.02;			// grid size in latitude direction
	double 	 LamInt=0.02;			// grid size in longitude direction
	double MinDatPhi=0.0;			// minimum latitude of data area
	double MinDatLam=0.0;			// minimum longitude of data area
	double       HC=0.0; 			// Cnm coefficient
    	double       HS=0.0; 			// Snm coefficient
    	double       Hc=0.0; 			// variance of Cnm coefficient
    	double       Hs=0.0; 			// variance of Snm coefficient
	double scale=pow(GM,2)/pow(a,4)*1e+10;  // scale factor for variance in mGal
    	double       t=0.0;			// cos(psi)
	double     phi=0.0;			// latitude of computational point [deg] 
    	double     lam=0.0;			// longitude of computational point [deg]
	double    phii=0.0;			// latitude of computational point [rad]
    	double    lami=0.0;			// longitude of computational point [rad]
    	double   total=0.0;			// total value
	double    slat=0.0;			// spherical latitude of computational point [rad]
    	double    radi=0.0;			// spherical radius
	double  coslon=0.0;			// cos(mLamda)
    	double  sinlon=0.0;			// sin(mLamda)
    	double     Rr1=0.0;			// R/r
    	double      Yn=0.0;			// Ymn
    	double   modif=0.0;			// modification part of Hotine's integral
	double      Q0=0.0;			// Molodenski coefficient
	double     QM0=0.0;			// Molodenski coefficient
	double    dwc1=0.0;			// first part of DWC effect
	double    dwc2=0.0;			// second part of DWC effect
	double    dwc3=0.0;			// third part of DWC effect
	double approxN=0.0;			// approximate geoid
	double  hotine=0.0;			// Hotine's integration
	double     H_t=0.0;			// Hotine's formula (closed form)
	double   gamma=0.0;			// normal gravity on ellipsoid
	double      N2=0.0;			// N2 part of Hotine's integration
	double 	   top=0.0;			// topographic effect 
	double 	   atm=0.0;			// atmospheric effect 
	double     ell=0.0;			// ellipsoidal effect 
	double     dwc=0.0;			// downward continuation effect 
	double   geoid=0.0;			// estimated geoid by Hotine's formula
	matris 	   C(imax);			// Cnm coefficients
	matris     S(imax);			// Snm coefficients
	matris 	  cs(imax);			// variance of Cnm
	matris 	  ss(imax);			// variance of Snm
	matris   c(Nmax+5);			// Degree variances
	matris  dc(Nmax+5);			// Error degree variances
	matris  PN(Nmax+9);			// Unnormalized Legendre polynomial
	matris   dg(mx,mx);			// terrestrial gravity disturbances
	matris    D(mx,mx);			// mean crust density 
	matris 	  lati(mx);			// latitude of running point
	matris    loni(mx);			// longitude of running point
	matris    H(mx,mx);			// topographic heights 
	matris 	   P(imax);			// Normalized Legendre Function
	matris  Dgr(mx,mx);			// gravity gradient
	matris  l_0(mx,mx);			// spherical distance
	matris  psi(mx,mx);			// computational psi between P and Q
	matris   HM(mx,mx);			// H^M(psi)*Blok Area	
  	matris 	 Q(Nmax+5); 		      	// Molodenski coefficient
	matris  qn(Nmax+5);	    		// sub-value
	matris   r(Nmax+5);    			// sub-value
	matris   l(Nmax+5);    			// "
	matris  ll(Nmax+5);	        	// "
	matris ind(Nmax+5);	        	// "
//** OPTION ANALYSIS ******************************************************************************/
	if(argc<4) {HELP();    exit(EXIT_FAILURE); }
	while((option=getopt(argc,argv,OPTIONS))!=-1)
        switch(option)
        {
		case 'G':
			model=fopen(optarg,"r");	
			break;
		case 'D':
			disturb=fopen(optarg,"r");
			break;
		case 'E':
			elevation=fopen(optarg,"r");	
                	break;
		case 'T':
			density=fopen(optarg,"r");
			if(density==NULL)
    			{
				printf("\nDensity file can not be opened!!!\n\n");
        			exit(EXIT_FAILURE);
			}
                	break;
		case 'M':
			M=atoi(optarg);
			break;
		case 'P':
			psi0=atof(optarg)/rho;
			break;
		case 'C':
			C0=atof(optarg);	
			break;
		case 'V':
			v=atoi(optarg);	
			break;
		case 'R':
			MinPhi=atof(strtok(optarg,slash));
			MaxPhi=atof(strtok(NULL,slash));
			MinLam=atof(strtok(NULL,slash));
			MaxLam=atof(strtok(NULL,slash));
                	break;
		case 'I':
                	PhiInt=atof(strtok(optarg,slash));
                	LamInt=atof(strtok(NULL,slash));
                	break;
		case 'S':
                	s=1;
			break;
		case 'H':
                	HELP();
                	exit(EXIT_SUCCESS);
		default:
                	HELP();
                	exit(EXIT_FAILURE);
        }
//***** READ FILES and DEFINE LIMITS **************************************************************/
	if(model==NULL)
    	{
		printf("\nGGM file can not be opened!!!\n\n");
        	exit(EXIT_FAILURE);
    	}
	if(disturb==NULL)
    	{
		printf("\nDisturbance file can not be opened!!!\n\n");
        	exit(EXIT_FAILURE);
    	}
	if(elevation==NULL)
    	{
		printf("\nElevation file can not be opened!!!\n\n");
        	exit(EXIT_FAILURE);
    	}
	while(fgets(line,256,model)!=NULL)
    	{
        	if(sscanf(line,"%i%i%lf%lf%lf%lf",&n,&m,&HC,&HS,&Hc,&Hs)==6)
        	{
       		        i=n*(n+1)/2+m;
                	C(i)=HC; 	S(i)=HS;	cs(i)=Hc;      	ss(i)=Hs;
        	}
    	}
	Mmax=n;
    	fclose(model);
    	if(C(0)==0.0)       C(0)=1.0;
	LevelEllipsoid levell(a,J2,GM,omega);// Create normal gravity field (GRS80) 
	for(i=0;i<11;i++)		// Compute harmonic coefficients of disturbing potential 
		C(2*i*i+i)-=levell.sphcoefs(i)/sqrt(4*i+1);
	MinDatPhi=MinPhi-4.0;
	MinDatLam=MinLam-4.0;
	while(!feof(disturb))
	{
		fscanf(disturb,"%lf%lf",&phi,&lam);
		i=round((phi-MinDatPhi)/PhiInt); 	j=round((lam-MinDatLam)/LamInt);
		lati(i)=phi/rho; 	loni(j)=lam/rho;
		fscanf(disturb,"%lf\n",&dg(i,j));
    	}
    	fclose(disturb);
	while(!feof(elevation))
    	{
		fscanf(elevation,"%lf%lf",&phi,&lam);
		i=round((phi-MinDatPhi)/PhiInt); 	j=round((lam-MinDatLam)/LamInt);
		fscanf(elevation,"%lf\n",&H(i,j));
    	}
    	fclose(elevation);
	if(density)
	{
		while(!feof(density))
    		{
			fscanf(density,"%lf%lf",&phi,&lam);
			i=round((phi-MinDatPhi)/PhiInt); 	j=round((lam-MinDatLam)/LamInt);
			fscanf(density,"%lf\n",&D(i,j));
    		}
    		fclose(density);
	}
// ***** PREPARATIONS TO COMPUTATION **************************************************************/
	matris E(Nmax+2,M+2);			// Paul's coefficient
	matris Sn(M);				// Sn coefficients 
	matris bn(M+3);				// Bn coefficients
	matris cosmlon(M+1);			// cos(mLongitute)
  	matris sinmlon(M+1);			// sin(mLongitute)
	matris     Dgn(M+2); 			// Dg[n]
	n=2; m=0;
	mmax=(Mmax+1)*(Mmax+2)/2;
    	for(i=3;i<=mmax;i++)
    	{
		if(n==m)
		{
	    		c(n)+=pow(C(i),2)+pow(S(i),2);
	    		dc(n)+=pow(cs(i),2)+pow(ss(i),2);
			c(n)=scale*(n+1.0)*(n+1.0)*c(n); 	// revisited
			dc(n)=scale*(n+1.0)*(n+1.0)*dc(n);	// revisited
	    		m=0; n++;
		}
		else
		{
	    		m++;
	    		c(n)+=pow(C(i),2)+pow(S(i),2); 
	    		dc(n)+=pow(cs(i),2)+pow(ss(i),2); 
		}
    	}
	for(n=Mmax+1;n<=Nmax;n++)				// Tcherning & Rapp (1974) Model
		c(n)=425.28*pow(0.999617,(n+2))*(n+1.0)/(n-2.0)/(n+24.0);  // for disturbance
	t=cos(psi0);
	PN(0)=1.0;
	PN(1)=t;
	for(n=2;n<=Nmax+2;n++)				// Unnormalized Legendre function
		PN(n)=((2.0*n-1.0)/n)*t*PN(n-1)-((n-1.0)/n)*PN(n-2);
	E(0,0)=t+1.0;
	E(1,1)=(t*t*t+1.0)/3.0; 
	E(2,1)=(6.0/5.0*t*(PN(3)-PN(1))-2.0/3.0*PN(2)*(PN(2)-1.0))/4.0;
	E(3,1)=((12.0/7.0)*PN(1)*(PN(4)-PN(2))-(2.0/3.0)*PN(3)*(PN(2)-PN(0)))/10.0; 
	E(2,2)=(9.0/10.0)*E(3,1)-(1.0/2.0)*E(2,0)+(3.0/5.0)*E(1,1); 
	E(4,2)=((PN(5)-PN(3))*PN(2)*20.0/9.0-(6.0/5.0)*PN(4)*(PN(3)-PN(1)))/14.0;
	E(3,3)=20.0/21.0*E(4,2)-2.0/3.0*E(3,1)+5.0/7.0*E(2,2);
	for(n=1;n<=M;n++)
 		E(n,0)=(PN(n+1)-PN(n-1))/(2.0*n+1.0);
	for(n=2;n<=M;n++)
		for(m=1;m<n;m++)
		  E(n,m)=E(m,n)=((PN(n+1)-PN(n-1))*PN(m)*(n*(n+1.))/(2.*n+1.)-((m*(m+1.))/(2.*m+1.))*PN(n)*(PN(m+1)-PN(m-1)))/((n-m)*(n+m+1.));
	for(n=M+1;n<=Nmax;n++)
		for(m=1;m<=M;m++)
		  E(n,m)=((PN(n+1)-PN(n-1))*PN(m)*(n*(n+1.))/(2.*n+1.)-((m*(m+1.))/(2.*m+1.))*PN(n)*(PN(m+1)-PN(m-1)))/((n-m)*(n+m+1.));
 	for(n=2;n<=M;n++)
	 	E(n,n)=(((n+1.0)*(2.0*n-1.0))/(n*(2.0*n+1.0)))*E(n+1,n-1)-((n-1.0)/n)*E(n,n-2)+((2.0*n-1.0)/(2.0*n+1.0))*E(n-1,n-1);
	r(0)=-2.0*log(1.0+sqrt((1.0-t)/2.0))+(1.0-t)*log(1.0+sqrt(2.0/(1.0-t)))-2.0*(1.0-sqrt((1.0-t)/2.0));
        r(1)=(1.0-(1.0-t)/2.)*((1.0-t)*log(1.0+sqrt(2.0/(1.0-t)))-1.)+2.0/3.0-((1.0-t)/3.0)*sqrt((1.0-t)/2.0);  
        l(0)=2.0-2.0*sqrt((1.0-t)/2.0);
	for(n=1;n<=Nmax+2;n++)
	{
		ind(n)=(1.0/(2.0*n+1.0))*(PN(n+1)-PN(n-1));
        	ll(n)=sqrt(2.0/(1.0-t))*((n+1.0)*ind(n)-(n-1.0)*ind(n-1))+(2.0*n-1.0)*l(n-1);
        	l(n)=ll(n)/(2.0*n+1.0);
	}
	for(n=2;n<=Nmax+2;n++)
	{
		r(n)=1./n*(n-1)*(n-2)*r(n-2)+1./(2.*n)*(l(n)-l(n-2))+(2.*n-1.)/n*(sqrt((1.-t)/2.)-1)*ind(n-1)+(2.*n-1.)/n*(1.-t*t)*PN(n-1)*log(1.+sqrt(2./(1.-t)));
		r(n)=r(n)/(n+1.0);
	}
	for(n=2;n<=Nmax;n++)
	{
		qn(n)=2.0*l(n)+r(n);
        	Q(n)=qn(n)-(ind(n)+3.0/(4.0*n+2.0)*((n+1.0)*ind(n+1)+n*ind(n-1)));
	}
// ** ESTIMATION OF MODIFICATION PARAMETERS *******************************************************/
	MOD_PAR(M,v,C0,c,dc,Q,E,Sn,bn);
// ** COMPUTATION OF GRAVITY GRADYENTS ************************************************************/
    	int framePhi=round(psi0*rho/PhiInt);									// vertical limit for compartment
    	int frameLam=round(acos((cos(psi0*rho/rho)-pow(sin(MaxPhi/rho),2))/(pow(cos(MaxPhi/rho),2)))*rho/LamInt);// horizontal limit for compartment
	double constant=PhiInt*LamInt/rho/rho;		// grid size of the block		
	for(phi=MinPhi-1.0;phi<=MaxPhi+1.0;phi=phi+PhiInt)
   	{
		lam=MinLam-1.0;
		in=round((phi-MinDatPhi)/PhiInt);		
		jn=round((lam-MinDatLam)/LamInt);
		for(i=in-framePhi;i<=in+framePhi;i++) 
		{
			for(j=jn;j<=jn+frameLam;j++)
			{
				if(i==in && j==jn) j++;
				x=i-in+framePhi; 	// position on the compartment
				y=abs(j-jn); 		// position on the compartment
				psi(x,y)=acos(sin(lati(in))*sin(lati(i))+cos(lati(in))*cos(lati(i))*cos(loni(j)-loni(jn)));
				if(psi(x,y)<0.008728) 	// cap size=0.5 degree for gravity gradient
					l_0(x,y)=pow(2.0*R*sin(psi(x,y)/2.0),3);
			}
		}
		for(lam=MinLam-1.0;lam<=MaxLam+1.0;lam=lam+LamInt)
		{
			total=0.0; 
			in=round((phi-MinDatPhi)/PhiInt);		
			jn=round((lam-MinDatLam)/LamInt);
			for(i=in-framePhi;i<=in+framePhi;i++) 
			{
				for(j=jn-frameLam;j<=jn+frameLam;j++)
				{
					if(i==in && j==jn) j++;
					x=i-in+framePhi; 	// position on the compartment
					y=abs(j-jn); 		// position on the compartment
					if(psi(x,y)<0.008728) 	// cap size=0.5 degree for gravity gradient
						total+=(dg(i,j)-dg(in,jn))/l_0(x,y)*cos(lati(i));
				}
			}
			total*=R*R/2.0/pi*constant;
			Dgr(in,jn)=total-2.0*dg(in,jn)/R;
		}
	}
// ** COMPUTATION OF APPROXIMATE GEOID HEIGHTS ****************************************************/
	mmax=(M+1)*(M+2)/2;
	cosmlon(0)=1.0;
	t=sin(psi0/2.0); total=0.0;
	Q0=-4.0*t+5.0*t*t+6.0*t*t*t-7.0*t*t*t*t+(6.0*t*t-6.0*t*t*t*t)*log((t)*(1.0+t));
	for(n=2;n<=M;n++)
		total+=(2.0*n+1.0)/2.0*bn(n-2)*E(n,0);
	QM0=Q0-total;
	framePhi=round(psi0*rho/PhiInt);							     // vertical limit for compartment
    	frameLam=round(acos((cos(psi0)-pow(sin(MaxPhi/rho),2))/(pow(cos(MaxPhi/rho),2)))*rho/LamInt);// horizontal limit for compartment
	for(phi=MinPhi;phi<=MaxPhi;phi=phi+PhiInt)
   	{
		lam=MinLam;
		in=round((phi-MinDatPhi)/PhiInt);		
		jn=round((lam-MinDatLam)/LamInt);
		for(i=in-framePhi;i<=in+framePhi;i++) 
		{
			for(j=jn;j<=jn+frameLam;j++)
			{
				if(i==in && j==jn) j++;
				x=i-in+framePhi; // position on the compartment
				y=abs(j-jn); // position on the compartment
				psi(x,y)=acos(sin(lati(in))*sin(lati(i))+cos(lati(in))*cos(lati(i))*cos(loni(j)-loni(jn))); //!!!
				if(psi(x,y)<=psi0)
				{
					total=0.0;
					PN(1)=t=cos(psi(x,y));
					H_t=1.0/sin(psi(x,y)/2.0)-log(1.0+1.0/sin(psi(x,y)/2.0)); //Hotine formula
					for(n=2;n<=M;n++)
					{
						PN(n)=((2.0*n-1.0)/n)*t*PN(n-1)-((n-1.0)/n)*PN(n-2);
						total+=(2.0*n+1.0)/2.0*bn(n-2)*PN(n);
					}
					HM(x,y)=(H_t-total)*cos(lati(i));
				}
			}
		}
		phii=phi/rho;	
		slat=phii;
		radi=0.0;
		levell.ell2sph(&slat,&radi);
		LEGENDRE(P,M,sin(slat));
		gamma=978032.67715*(1.0+0.001931851353*pow(sin(phii),2))/sqrt(1.0-0.006694380023*pow(sin(phii),2));
		for(lam=MinLam;lam<=MaxLam;lam=lam+LamInt)
		{
			lami=lam/rho;
			in=round((phi-MinDatPhi)/PhiInt);		
			jn=round((lam-MinDatLam)/LamInt);
			for(i=in-framePhi;i<=in+framePhi;i++) 
			{
				for(j=jn-frameLam;j<=jn+frameLam;j++)
				{
					if(i==in && j==jn) j++;
					x=i-in+framePhi; // position on the compartment
					y=abs(j-jn); // position on the compartment
					if(psi(x,y)<=psi0)
					{
						hotine+=HM(x,y)*(dg(i,j)-dg(in,jn));
						atm+=HM(x,y)*H(in,jn);
						dwc3+=HM(x,y)*(H(in,jn)-H(i,j))*Dgr(i,j);
					}
				}
			}
			hotine*=R/4.0/pi/gamma*constant;
			N2=R/2.0/gamma*dg(in,jn)*QM0;
// ** COMPUTATION OF gd^GGM ***********************************************************************/
			cosmlon(1)=coslon=cos(lami);
			sinmlon(1)=sinlon=sin(lami);
			m=1;
			while(++m<=M)
			{
				cosmlon(m)=2.0*coslon*cosmlon(m-1)-cosmlon(m-2);
				sinmlon(m)=2.0*coslon*sinmlon(m-1)-sinmlon(m-2);
			}
			Rr1=a/radi;
			n=2; m=0; i=3;
			while(i<mmax)
			{
				Yn+=P(i)*(C(i)*cosmlon(m)+S(i)*sinmlon(m));
				if(m==n)
				{
					Dgn(n)= 1.0e+5*GM/a/a*pow(Rr1,n+2)*(n+1.0)*Yn;
					Yn=0.0;	
					n++; m=0;
				}
				else   m++; 
				i++;
			}
			for(n=2;n<=M;n++)
				modif+=bn(n-2)*Dgn(n);
			modif*=R/2.0/gamma;
			approxN=hotine-N2+modif;
// ** COMPUTATIONS OF ADDITIVE CORRECTIONS ********************************************************/
			if(density) top=-2.0e+5*pi*G*D(in,jn)/gamma*(pow(H(in,jn),2)+2.0*pow(H(in,jn),3)/3.0/R);
			else 	    top=-2.0e+5*pi*G*2670.0/gamma*(pow(H(in,jn),2)+2.0*pow(H(in,jn),3)/3.0/R);
			atm=-1.0e+5*atm*constant*G*1.23*R/gamma;
			dwc1=H(in,jn)*(dg(in,jn)/gamma+approxN/(R+H(in,jn))-Dgr(in,jn)*H(in,jn)/2.0/gamma);
			for(n=2;n<=M;n++)
				dwc2+=bn(n-2)*(pow(R/(R+H(in,jn)),n+2)-1.0)*Dgn(n);
			dwc2*=R/2.0/gamma;
			dwc3*=R/4.0/gamma/pi*constant;
			dwc=dwc1+dwc2+dwc3;
			ell=((0.0036-0.0109*pow(sin(slat),2))*dg(in,jn)+0.005*approxN*pow(cos(slat),2))*QM0;
			geoid=approxN+top+atm+dwc+ell;
			if(s) printf("%13.8f%13.8f%9.4f%8.4f%8.4f%8.4f%8.4f%9.4f\n",phi,lam,approxN,top,atm,dwc,ell,geoid);
			else  printf("%13.8f%13.8f%9.4f\n",phi,lam,geoid);
			hotine=modif=atm=dwc2=dwc3=0.0;
		}
	}
	return 0;
}// ** FINISH *************************************************************************************/
void MOD_PAR(int M,int v,double C0,matris c,matris dc,matris Q,matris E,matris &Sn,matris &bn)
{
	int	       n=0;			// degree
	int 	       k=0;			// degree
	int 	       r=0;			// order
	double  mu=0.99899012911838605;		// Constant for terrestrial data	
	double	cT=C0/(mu*mu);			// Constant for terrestrial data
	double   total=0.0;			// total value
	matris  ST(Nmax+2);			// Sigma Terrestrial data 
	matris   q(Nmax+2);       		// qn coefficients
	matris      h(M-1);   		   	// observational vector  
	matris  A(M-1,M-1);			// Design matrix 
	matris  QM(Nmax+2); 		      	// Molodenski's Truncation Coefficient
	for(n=2;n<=Nmax;n++)
		ST(n)=cT*(1.0-mu)*pow(mu,n);
	if(v==1) // computation of coefficients Sn, bn for "Biased" version  
	{
		for(n=2;n<=Nmax;n++) 	
			q(n)=ST(n)+c(n);
		for(k=2;k<=M;k++) 
		{
			total=0.0;
			for(n=2;n<=Nmax;n++)
				total+=(Q(n)*q(n)-2.0*ST(n)/(n+1.0))*E(n,k); // for disturb
			h(k-2)=2.0*ST(k)/(k+1.0)-Q(k)*ST(k)+total*(2.0*k+1.0)/2.0;
			for(r=k;r<=M;r++) 
			{
				total=0.0;
				for(n=2;n<=Nmax;n++) 
					total+=E(n,k)*E(n,r)*q(n);
				if(k==r) A(r-2,k-2)=ST(r)+dc(r)-(2.0*r+1.0)/2.0*E(k,r)*ST(r)-(2.0*k+1.0)/2.0*E(r,k)*ST(k)+(2.0*k+1.0)/2.0*(2.0*r+1.0)/2.0*total;
				else     A(r-2,k-2)=A(k-2,r-2)=-(2.0*r+1.0)/2.0*E(k,r)*ST(r)-(2.0*k+1.0)/2.0*E(r,k)*ST(k)+(2.0*k+1.0)/2.0*(2.0*r+1.0)/2.0*total;
			}
		}
		Sn=bn=invch(A)*h;
	}
	if(v==2) // computation of coefficients Sn, bn for "Unbiased" version 
	{
		for(n=2;n<=M;n++)      
			q(n)=ST(n)+dc(n);
		for(n=M+1;n<=Nmax;n++) 
			q(n)=ST(n)+c(n);
		for(k=2;k<=M;k++) 
		{
			total=0.0;
			for(n=2;n<=Nmax;n++) 
				total+=(Q(n)*q(n)-2.0*ST(n)/(n+1.0))*E(n,k); // for disturb
			h(k-2)=2.0*ST(k)/(k+1.0)-Q(k)*q(k)+total*(2.0*k+1.0)/2.0;
			for(r=k;r<=M;r++) 
			{
				total=0.0; 
				for(n=2;n<=Nmax;n++) 
					total+=E(n,k)*E(n,r)*q(n);
				if(k==r)    A(r-2,k-2)=q(k)-(2.0*r+1.0)/2.0*E(k,r)*q(r)-(2.0*k+1.0)/2.0*E(r,k)*q(k)+(2.0*k+1.0)/2.0*(2.0*r+1.0)/2.0*total;
				else A(r-2,k-2)=A(k-2,r-2)=-(2.0*r+1.0)/2.0*E(k,r)*q(r)-(2.0*k+1.0)/2.0*E(r,k)*q(k)+(2.0*k+1.0)/2.0*(2.0*r+1.0)/2.0*total;
			}
		}
    		Sn=solvsvd(A,h);
		for(n=2;n<=M;n++)
    		{
			total=0.0;
	    		for(k=2;k<=M;k++)
				total+=(2.0*k+1.0)/2.0*Sn(k-2)*E(n,k);
    			QM(n)=Q(n)-total;
			bn(n-2)=Sn(n-2)+QM(n);
    		}
	}
	if(v==3) // computation of coefficients Sn, bn for "Optimum" version 
	{
		for(n=2;n<=M;n++)      
			q(n)=ST(n)+c(n)*dc(n)/(c(n)+dc(n));
		for(n=M+1;n<=Nmax;n++) 
			q(n)=ST(n)+c(n);
		for(k=2;k<=M;k++) 
		{
			total=0.0;
			for(n=2;n<=Nmax;n++) 
				total+=(Q(n)*q(n)-2.0*ST(n)/(n+1.0))*E(n,k); // for disturb
			h(k-2)=2.0*ST(k)/(k+1.0)-Q(k)*q(k)+total*(2.0*k+1.0)/2.0;
			for(r=k;r<=M;r++) 
			{
				total=0.0;
				for(n=2;n<=Nmax;n++) 
					total+=E(n,k)*E(n,r)*q(n);
				if(k==r) A(r-2,k-2)=q(k)-(2.0*r+1.0)/2.0*E(k,r)*q(r)-(2.0*k+1.0)/2.0*E(r,k)*q(k)+(2.0*k+1.0)/2.0*(2.0*r+1.0)/2.0*total;
				else A(r-2,k-2)=A(k-2,r-2)=-(2.0*r+1.0)/2.0*E(k,r)*q(r)-(2.0*k+1.0)/2.0*E(r,k)*q(k)+(2.0*k+1.0)/2.0*(2.0*r+1.0)/2.0*total;
			}
		}
    		Sn=solvsvd(A,h);
		for(n=2;n<=M;n++)
    		{
			total=0.0;
	    		for(k=2;k<=M;k++)
				total+=(2.0*k+1.0)/2.0*Sn(k-2)*E(n,k);
    			QM(n)=Q(n)-total;
			bn(n-2)=(Sn(n-2)+QM(n))*c(n)/(c(n)+dc(n));
    		}
	}
}
void LEGENDRE(matris &P,int N,double x)
{
	int i=0;
	int j=0;
	int k=0;
	int n=0;
	int m=0;
	int imax=(N+1)*(N+2)/2;
	double sinx=x; 
	double cosx=sqrt(1-x*x);
	double f1=0.0;
	double f2=0.0;
	double f3=0.0;
	double f4=0.0;
	double f5=0.0;
	P(0)=1.0;
	P(1)=sqrt(3.0)*sinx;
	P(2)=sqrt(3.0)*cosx;
	for(n=2;n<=N;n++)
	{
		i=n*(n+1)/2+n;				// index for Pn,n
		j=i-n-1;				// index for Pn-1,n-1
		f1=sqrt((2.0*n+1)/2/n);
		f2=sqrt(2.0*n+1);
		P(i)=f1*cosx*P(j);			// diagonal elements
		P(i-1)=f2*sinx*P(j);			// subdiagonal elements
	}
	n=2; m=0; i=2;
	while(++i<imax-2)
	{
		if(m==n-1)
		{
			m=0; n++; i++;
		}
		else
		{
			j=i-n;						// index for Pn-1,m
			k=i-2*n+1;					// index for Pn-2,m
			f3=sqrt((2.0*n+1)/(n-m)/(n+m));
			f4=sqrt(2.0*n-1);
			f5=sqrt((n-m-1.0)*(n+m-1.0)/(2.0*n-3));
			P(i)=f3*(f4*sinx*P(i-n)-f5*P(k));
			m++;
		}
	}
}
void HELP()
{
	fprintf(stderr,"\n     The LSMHSOFT computes gravimetric geoid by the least squares modified Hotine's formula.\n\n");
	fprintf(stderr,"USAGE:\n");
	fprintf(stderr,"     LSMHSOFT -G[model<file>] -D[disturbance<file>] -E[elevation<file>] -T[density<file>] ...\n");
	fprintf(stderr,"             ... -M[<value>] -P[<value>] -C[<value>] -V[<value>] -R[<value>] -I[<value>] -S -H\n\n");
	fprintf(stderr,"PARAMETERS:\n");
	fprintf(stderr,"     model:     global geopotential model that will be used as a reference model.\n");
	fprintf(stderr,"                It includes harmonic coefficients in GFZ format (n,m,Cnm,Snm,sigmaC,sigmaS).\n\n");
	fprintf(stderr,"     disturb:   mean gravity disturbances which cover the data area.\n");
	fprintf(stderr,"                It includes grid based data (geodetic latitude, longitude and mean disturbance).\n\n");
	fprintf(stderr,"     elevation: mean topographic elevations which cover the data area.\n");
	fprintf(stderr,"                It includes grid based DEM data (geodetic latitude, longitude and mean elevation).\n\n");
	fprintf(stderr,"OPTIONS:\n");
	fprintf(stderr,"     density:   mean topographic densities which cover the target area.\n");
	fprintf(stderr,"                It includes grid based data (geodetic latitude, longitude and mean density).\n\n");
	fprintf(stderr,"     -M<value>  maximum expansion of the GGM used in the computation\n");
	fprintf(stderr,"                default: 195\n\n");
	fprintf(stderr,"     -P<value>  integration cap size (unit: degree)\n");
	fprintf(stderr,"                default: 0.5\n\n");
	fprintf(stderr,"     -C<value>  variance of terrestrial gravity data (unit: mGal^2)\n");
	fprintf(stderr,"                default: 1.0 \n\n");
	fprintf(stderr,"     -V<value>  version of the LSMH method (biased=1, unbiased=2, optimum=3)\n");
	fprintf(stderr,"                default: 1\n\n");
	fprintf(stderr,"     -R<value>  limits of the target area (MinLat/MaxLat/MinLon/MaxLon)\n");
	fprintf(stderr,"                default: 45.01/46.99/1.01/4.99 (2x4 degrees)\n\n");
	fprintf(stderr,"     -I<value>  intervals of the grid (LatInterval/LonInterval)\n");
	fprintf(stderr,"                default: 0.02/0.02 (72x72 arc-seconds)\n\n");
	fprintf(stderr,"     -S         prints all segments (lat, lon, Ñ, top, atm, dwc, ell, N), respectively.\n\n");
	fprintf(stderr,"     -H         prints this help\n\n");
	fprintf(stderr,"Developed by:   Dr. R. Alpay Abbak\n\n");
}
