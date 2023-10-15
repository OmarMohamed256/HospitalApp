import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SpecialityService } from 'src/app/core/services/speciality.service';
import { Speciality } from 'src/app/models/speciality';

@Component({
  selector: 'app-specialities-model',
  templateUrl: './specialities-model.component.html',
  styleUrls: ['./specialities-model.component.scss']
})
export class SpecialitiesModelComponent implements OnInit {
  @Input() visible: boolean = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() specialityCreated = new EventEmitter<Speciality>();

  createUpdateSpecialityForm!: FormGroup;
  @Input() speciality: Speciality = {
    id: 0,
    name: ''
  }

  constructor(private fb: FormBuilder, private specialityService: SpecialityService) {
  }

  ngOnInit(): void {
    this.intializeForm();
  }

  createUpdateSpeciality() {
    if (this.createUpdateSpecialityForm.get('id')?.value == 0) {
      this.createSpeciality();
    } else {
      this.updateSpeciality();
    }
    this.modelToggeled(false);
  }

  updateSpeciality() {
    this.specialityService.updateSpeciality(this.createUpdateSpecialityForm.value).subscribe(response => {
      this.specialityCreated.emit(response);
    })
  }

  createSpeciality() {
    this.specialityService.createSpeciality(this.createUpdateSpecialityForm.value).subscribe(response => {
      this.specialityCreated.emit(response);
    })
  }

  modelToggeled(e: any) {
    this.visible = e;
    this.visibleChange.emit(this.visible);
  }

  intializeForm() {
    this.createUpdateSpecialityForm = this.fb.group({
      id: [this.speciality.id, Validators.required],
      name: [this.speciality.name, Validators.required],
    });
  }

}
