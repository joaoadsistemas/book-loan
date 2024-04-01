import { ToastrService } from 'ngx-toastr';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../_services/user.service';
import { inject } from '@angular/core';
import { map } from 'rxjs';

export const adminGuard: CanActivateFn = (route, state) => {
  const userService = inject(UserService);
  const toastr = inject(ToastrService);
  const router = inject(Router);

  return userService.userLoggedToken$.pipe(
    map((user) => {
      if (user) {
        if (!userService.isAdmin()) {
          toastr.error('You dont have permission for access this page');
          router.navigate(['/noauthorization']);
          return false;
        }

        return true;
      } else {
        toastr.error('You dont have permission for access this page');
        router.navigate(['/login']);
        return false;
      }
    })
  );
};
