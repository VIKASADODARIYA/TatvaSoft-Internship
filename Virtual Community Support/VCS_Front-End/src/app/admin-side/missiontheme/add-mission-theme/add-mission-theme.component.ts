import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AdminsideServiceService } from 'src/app/service/adminside-service.service';
import ValidateForm from 'src/app/Helper/ValidateForm';

@Component({
  selector: 'app-add-mission-theme',
  templateUrl: './add-mission-theme.component.html',
  styleUrls: ['./add-mission-theme.component.css']
})
export class AddMissionThemeComponent implements OnInit {
  addMissionThemeForm: FormGroup;
  themeId: any;
  editData: any;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private toast: NgToastService,
    private service: AdminsideServiceService,
    private activeRoute: ActivatedRoute
  ) {
    this.themeId = this.activeRoute.snapshot.paramMap.get('Id');
  }

  ngOnInit(): void {
    this.initMissionThemeForm();
    if (this.themeId) {
      this.fetchDataById(this.themeId);
    }
  }

  initMissionThemeForm(): void {
    this.addMissionThemeForm = this.fb.group({
      id: [0],
      themeName: ['', Validators.required],
      status: ['', Validators.required]
    });
  }

  fetchDataById(id: any): void {
    this.service.MissionThemeById(id).subscribe(
      (data: any) => {
        if (data.result === 1) {
          this.editData = data.data;
          this.addMissionThemeForm.patchValue(this.editData);
        } else {
          this.toast.error({ detail: 'ERROR', summary: data.message, duration: 3000 });
        }
      },
      (err) => this.toast.error({ detail: 'ERROR', summary: err.message, duration: 3000 })
    );
  }

  OnSubmit(): void {
    const value = this.addMissionThemeForm.value;
    if (this.addMissionThemeForm.valid) {
      if (value.id === 0) {
        this.insertData(value);
      } else {
        this.updateData(value);
      }
    } else {
      ValidateForm.ValidateAllFormFields(this.addMissionThemeForm);
    }
  }

  insertData(value: any): void {
    this.service.AddMissionTheme(value).subscribe(
      (data: any) => {
        if (data.result === 1) {
          this.toast.success({ detail: 'SUCCESS', summary: data.data, duration: 3000 });
          setTimeout(() => {
            this.router.navigate(['admin/missionTheme']);
          }, 1000);
        } else {
          this.toast.error({ detail: 'ERROR', summary: data.message, duration: 3000 });
        }
      },
      (err) => this.toast.error({ detail: 'ERROR', summary: err.message, duration: 3000 })
    );
  }

  updateData(value: any): void {
    this.service.UpdateMissionTheme(value).subscribe(
      (data: any) => {
        if (data.result === 1) {
          this.toast.success({ detail: 'SUCCESS', summary: data.data, duration: 3000 });
          setTimeout(() => {
            this.router.navigate(['admin/missionTheme']);
          }, 1000);
        } else {
          this.toast.error({ detail: 'ERROR', summary: data.message, duration: 3000 });
        }
      },
      (err) => this.toast.error({ detail: 'ERROR', summary: err.message, duration: 3000 })
    );
  }

  OnCancel(): void {
    this.router.navigateByUrl('admin/missionTheme');
  }
}
