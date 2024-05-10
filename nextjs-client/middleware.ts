import { NextRequest, NextResponse } from 'next/server';

export default async function middleware(req: NextRequest) {
  // const protectedRoutes = ['/', '/dashboard'];
  const publicRoutes = ['/login', '/signup', '/favicon.ico', '/register'];
  const path = req.nextUrl.pathname;
  // const isProtectedRoute = protectedRoutes.includes(path);
  const isPublicRoute = publicRoutes.includes(path);
  const jwtToken = req.cookies.get('jwtToken')?.value;
  console.log(jwtToken);
  console.log(path);

  const tenantClaims = req.cookies.get('tenantClaims')?.value;

  if (!jwtToken && !isPublicRoute) {
    console.log('User is not authenticated');
    return NextResponse.redirect(new URL('/login', req.nextUrl));
  }

  // check if cookie is valid too?
  // you cannot make requests to backend with invalid cookie, but frontend might need to handle it so it does not look weird
  if (jwtToken && path == '/login') {
    return NextResponse.redirect(new URL(`/`, req.nextUrl));
  }

  if (jwtToken && path == '/logout') {
    const response = NextResponse.redirect(new URL(`/login`, req.nextUrl));
    response.cookies.delete('jwtToken');
    return response;
  }

  return NextResponse.next();
}

export const config = {
  matcher: [
    // '/((?!api|_next/static|_next/image|.*\\.png$).*)',
    '/',
    // '/login',
    '/dashboard',
    '/:path/',
    '/:path/productionunits',
    '/:path/:path/dashboard',
    '/:path/:path/invoice',
    '/:path/:path/customers',
  ],
};
