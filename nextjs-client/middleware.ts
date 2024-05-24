import { NextRequest, NextResponse } from 'next/server';

export default async function middleware(req: NextRequest) {
  const publicRoutes = ['/login', '/signup', '/favicon.ico', '/register', '/'];
  const path = req.nextUrl.pathname;
  const isPublicRoute = publicRoutes.includes(path);
  const jwtToken = req.cookies.get('jwtToken')?.value;

  if(jwtToken && path == "/logout"){
    console.log("have jwt and visit logout")
    const response = NextResponse.redirect(new URL(`/login`, req.nextUrl));
    response.cookies.delete('jwtToken');
    return response;
  }

  if (!jwtToken && !isPublicRoute) {
    console.log("dont have jwt and visit non public")

    return NextResponse.redirect(new URL('/login', req.nextUrl));
  }

  // check if cookie is valid too?
  // you cannot make requests to backend with invalid cookie, but frontend might need to handle it so it does not look weird
  if(jwtToken && path == "/login"){
    console.log("have jwt and visit login")
    
    // TODO: add currentTenant to response from server?
    // var currentTenant = localStorage.getItem("currentTenant")!;
    return NextResponse.redirect(new URL(`/${"tenant"}`, req.nextUrl));

  }

  

  return NextResponse.next();
}

export const config = {
  matcher: [
    // '/((?!api|_next/static|_next/image|.*\\.png$).*)',
    // '/login',
    '/home',
    '/:path/',
    '/:path/productionunits',
    '/:path/:path/home',
    '/:path/:path/invoice',
    '/:path/:path/students',
  ],
};
