#include <stdio.h>
#include <stdlib.h>
#include <sys/mman.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <unistd.h>
#include <fcntl.h>
#include <sys/time.h>
#include <string.h>

int kp_test_openw(const char *name);
int kp_test_openr(const char *name);
int kp_test_close(int fd);
void* kp_test_mmap_write( int d, int size );
void* kp_test_mmap_read( int d, int size );
int kp_test_munamp( void *a, int size );
int kp_test_usemaped( void *a, void *b, int size );

int kp_test_openw(const char *name){
   int dskr;
   dskr = open( name, O_RDWR | O_CREAT | O_TRUNC, 0640 );
   if( dskr == -1 ){
      perror( name );
      exit( 255 );
   }
   printf( "dskr = %d\n", dskr );
   return dskr;
}

int kp_test_openr(const char *name){
   int dskr;
   dskr = open( name, O_RDONLY);
   if( dskr == -1 ){
      perror( name );
      exit( 255 );
   }
   printf( "dskr = %d\n", dskr );
   return dskr;
}

int kp_test_close(int fd){
   int rv;
   rv = close( fd );
   if( rv != 0 ) perror ( "close() failed" );
   else puts( "closed" );
   return rv;
}

void* kp_test_mmap_write( int d, int size ){
   void *a = NULL;
   a = mmap( NULL, size, PROT_WRITE, MAP_SHARED, d, 0 );
   if( a == MAP_FAILED ){
      perror( "mmap failed" );
      abort();
   }
   else{
     printf("%d mapped\n", d);
  }
   return a;
}


void* kp_test_mmap_read( int d, int size ){
   void *a = NULL;
   a = mmap( NULL, size, PROT_READ, MAP_SHARED, d, 0 );
   if( a == MAP_FAILED ){
      perror( "mmap failed" );
      abort();
   }
   else{
     printf("%d mapped\n", d);
  }
   return a;
}


int kp_test_munamp( void *a, int size ){
   int rv;
   rv = munmap( a, size );
   if( rv != 0 ){
      puts( "munmap failed" );
      abort();
   }
   return 1;
}
int kp_test_usemaped( void *a, void *b,  int size ){
   memcpy( a, b, size );
   return 1;
}
int main( int argc, char * argv[] ){
   int d1;
   int d2;
   void *a1 = NULL;
   void *a2 = NULL;
   if( argc != 3 ){
      printf( "Netinkamas argumentu kiekis \n" );
      exit( 255 );
   }
   d1 = kp_test_openr( argv[1] );
   d2 = kp_test_openw( argv[2] );
   


 struct stat st;
   stat(argv[1], &st);
   int SIZE = st.st_size;
   printf("%d\n", SIZE); 

  ftruncate(d2, SIZE);

   a1 = kp_test_mmap_read( d1, SIZE );
   a2 = kp_test_mmap_write( d2, SIZE );
   
   kp_test_usemaped( a2, a1, SIZE );
   
   kp_test_munamp( a1, SIZE );
   kp_test_munamp( a2, SIZE );

   kp_test_close( d1 );
   kp_test_close( d2 );
   return 0;
}
