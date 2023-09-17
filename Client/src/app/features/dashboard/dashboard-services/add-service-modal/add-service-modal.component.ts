import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-service-modal',
  templateUrl: './add-service-modal.component.html',
  styleUrls: ['./add-service-modal.component.scss']
})
export class AddServiceModalComponent implements OnInit {
  @Input() visible: boolean = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  specialityList = [];
  createServiceForm!: FormGroup;

  constructor(private fb: FormBuilder) {
  }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  modelToggeled(e: any) {
    this.visible = e;
    this.visibleChange.emit(this.visible);
  }

  intializeForm() {
    this.createServiceForm = this.fb.group({
      name: ['', Validators.required],
      doctorPercentage: ['', Validators.required],
      hospitalPercentage: ['', Validators.required],
      disposablesPercentage: ['', Validators.required],
      totalPrice: ['', Validators.required],
      serviceSpecialityId: ['', Validators.required]
    })
  }
}
