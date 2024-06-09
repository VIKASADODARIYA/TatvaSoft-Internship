import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AdminsideServiceService } from 'src/app/service/adminside-service.service';
import { AdminloginService } from 'src/app/service/adminlogin.service';

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.css']
})
export class UpdateUserComponent implements OnInit {

  constructor(public fb: FormBuilder, private service: AdminloginService, private toastr: ToastrService, public activateRoute: ActivatedRoute, private router: Router, public toast: NgToastService) { }
  updateForm: FormGroup;
  formValid: boolean = false;
  userId: string; // Store the user ID
  updateData: any;

  ngOnInit(): void {
    // Initialize updateForm as an empty FormGroup instance
    this.updateForm = this.fb.group({
      id: [''],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(10)]],
      emailAddress: ['', [Validators.required, Validators.email]],
      userType: ['user', Validators.required],
      password: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(10)]],
      confirmPassword: ['', Validators.required]
    });

    // Extract user ID from route params
    this.userId = this.activateRoute.snapshot.paramMap.get('Id');
    if (this.userId) {
      // Call method to fetch user data by ID
      this.FetchDetail(this.userId);
    }
  }

  passwordCompareValidator(fc: AbstractControl): ValidationErrors | null {
    return fc.get('password')?.value === fc.get('confirmPassword')?.value ? null : { notmatched: true }
  }

  get firstName() {
    return this.updateForm.get('firstName') as FormControl;
  }

  get lastName() {
    return this.updateForm.get('lastName') as FormControl;
  }

  get phoneNumber() {
    return this.updateForm.get('phoneNumber') as FormControl;
  }

  get emailAddress() {
    return this.updateForm.get('emailAddress') as FormControl;
  }

  get userType() {
    return this.updateForm.get('userType') as FormControl;
  }

  get password() {
    return this.updateForm.get('password') as FormControl;
  }

  get confirmPassword() {
    return this.updateForm.get('confirmPassword') as FormControl;
  }

  // Define getters for other form controls

  FetchDetail(id: any) {
    this.service.GetUserById(id).subscribe((data: any) => {
      this.updateData = data.data;
      this.updateForm.setValue({
        id: this.updateData.id,
        firstName: this.updateData.firstName,
        lastName: this.updateData.lastName,
        phoneNumber: this.updateData.phoneNumber,
        emailAddress: this.updateData.emailAddress,
        userType: this.updateData.userType || 'user',
        password: this.updateData.password,
        confirmPassword: this.updateData.confirmPassword
      });
    });
  }

  onSubmit() {
    this.formValid = true;
    if (this.updateForm.valid) {
      let updatedUserData = this.updateForm.value;
      console.log(updatedUserData)
      this.service.UpdateUser(updatedUserData).subscribe((data: any) => {
        if (data.result == 1) {
          this.toast.success({ detail: "SUCCESS", summary: data.data, duration: 3000 });
          setTimeout(() => {
            this.router.navigate(['userPage']);
          }, 1000);
        } else {
          this.toastr.error(data.message);
        }
      }, err => this.toast.error({ detail: "ERROR", summary: err.message, duration: 3000 }));
      this.formValid = false;
    }
  }
}
