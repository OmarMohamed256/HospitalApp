import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { GenderList } from 'src/app/constants/genders';
import { UserService } from 'src/app/core/services/user.service';
import { UserData } from 'src/app/models/UserModels/userData';
import { FileUploadModalComponent } from '../file-upload-modal/file-upload-modal.component';
import { Image } from 'src/app/models/ImageModels/image';
import { GalleryItem, ImageItem } from 'ng-gallery';

@Component({
  selector: 'app-patient-info',
  templateUrl: './patient-info.component.html',
  styleUrls: ['./patient-info.component.scss'],
})
export class PatientInfoComponent implements OnInit {
  user: UserData | undefined;
  genderList = GenderList;
  updateUserForm!: FormGroup;
  validationErrors: string[] = [];
  activePane = 0;
  imageActivePane = 0;
  modalVisibility: boolean = false;
  selectedImageCategory = 'lab_test';
  images: Image[] = [];
  galleryImages: GalleryItem[] = [];

  @ViewChild(FileUploadModalComponent) fileUploadModal!: FileUploadModalComponent;

  constructor(private route: ActivatedRoute, private fb: FormBuilder,
     private userService: UserService, private toastr: ToastrService) {
  }

  openModal() {
    this.modalVisibility = !this.modalVisibility
  }

  intializeForm() {
    this.updateUserForm = this.fb.group({
      username: [{value: '', disabled: true}, Validators.required],
      email: [{value: '', disabled: true}, Validators.required],
      age: ['', Validators.required],
      fullname: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      gender: ['', Validators.required]
    })

    if (this.user) {
      this.updateUserForm.setValue({
        username: this.user.username,
        email: this.user.email,
        age: this.user.age,
        fullname: this.user.fullName,
        phoneNumber: this.user.phoneNumber,
        gender: this.user.gender
      });
    }
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.user = data['user'];
      this.intializeForm();
      this.getUserImages();
    })
  }

  updateUser() {
    this.user!.age = this.updateUserForm.value.age
    this.user!.fullName = this.updateUserForm.value.fullname
    this.user!.phoneNumber = this.updateUserForm.value.phoneNumber
    this.user!.gender = this.updateUserForm.value.gender

    this.userService.updateUser(this.user!).subscribe({
      next: (response) => {
        this.toastr.success("User Updated Sucessfully")
      },
      error: (error) => {
        this.validationErrors = error;
      }
    });
  }
  getUserImages() {
    this.userService.getUserImages(this.user?.id!).subscribe(response => {
      this.images = response;
      this.filterGalleryImages();
    })
  }
  filterGalleryImages() {
    this.galleryImages = this.images
    .filter(image => image.category === this.selectedImageCategory)
    .map(image => {
      return new ImageItem({ src: image.url, thumb: image.url }); // replace with actual thumbnail URL if available
    });
  }
  onTabChange($event: number) {
    this.activePane = $event;
  }
  onImageChange($event: number) {
    if ($event === 0) {
      this.selectedImageCategory = 'lab_test';
      this.filterGalleryImages();
    } else if ($event === 1) {
      this.selectedImageCategory = 'radiology';
      this.filterGalleryImages();
    } else if ($event === 2) {
      this.selectedImageCategory = 'official_documents';
      this.filterGalleryImages();
    }
    this.imageActivePane = $event;
  }
}
