import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../_services/user.service';
import { map } from 'rxjs';

export const verifyLoginGuard: CanActivateFn = (route, state) => {
  const userService = inject(UserService);
  const router = inject(Router)

  return userService.userLoggedToken$.pipe(
    map((user) => {
      if (user) {
        router.navigate(['/'])
        return false;
      } else {
        return true;
      }
    })
  );
};
