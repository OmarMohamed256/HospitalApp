import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserService } from 'src/app/core/services/user.service';
import { Image } from 'src/app/models/ImageModels/image';

@Component({
  selector: 'app-file-upload-modal',
  templateUrl: './file-upload-modal.component.html',
  styleUrls: ['./file-upload-modal.component.scss']
})
export class FileUploadModalComponent implements OnInit {
  @Input() visible: boolean = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Input() userId = '';
  @Input() category = 'lab_test';
  @Output() imageAdded = new EventEmitter<Image>();
  uploadImageForm!: FormGroup;

  constructor(private fb: FormBuilder, private userService: UserService) {
  }
  ngOnInit(): void {
    this.intializeForm();
  }

  intializeForm() {
    this.uploadImageForm = this.fb.group({
      userId: [this.userId, Validators.required],
      category: [this.category, Validators.required],
      imageDate: ['', Validators.required],
      fileSource: ['', Validators.required],
      file: '',
      type: [''],
      organ: [''],
    });
  }
  modelToggeled(e: any) {
    this.visible = e;
    this.visibleChange.emit(this.visible);
  }
  addImage() {
    console.log(this.uploadImageForm.get('file')?.value)
    this.uploadImageForm.get('category')?.setValue(this.category);
    this.uploadImage();
  }
  uploadImage() {
    this.userService.uploadImage(this.uploadImageForm.value).subscribe(response => {
      this.imageAdded.emit(response);
    })
  }

  onFileSelected(event: any) {
    if(event.target.files.length > 0) 
     {
       this.uploadImageForm.patchValue({
          file: event.target.files[0],
       })
     }
  }
}
