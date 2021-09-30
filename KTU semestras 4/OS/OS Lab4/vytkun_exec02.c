/* Vytenis Kunickas IFF-7/2 vytkun */
/* Failas: vytkun_exec02.c */

#include <stdio.h>
#include <unistd.h>
#include <signal.h>
#include <unistd.h>

int vp_test();

int vp_test()
{
	execl("/bin/touch", "touch", "/export/home/vytkun/e02skriptas.sh", (char*)0);
	system("echo \"#!/bin/sh\" > /export/home/vytkun/e02skriptas.sh");
	system("echo \"echo hello\" > /export/home/vytkun/e02skriptas.sh");
	execl("/bin/chmod", "chmod", "u+x", "/export/home/vytkun/e02skriptas.sh", (char*)0);
	execl("/export/home/vytkun/e02skriptas.sh", "e02skriptas.sh", (char*)0);
}

int main( int argc, char * argv[] )
{
   printf( "(C) 2019 Vytenis Kunickas, %s\n", __FILE__ );
   vp_test();
   return 0;
}