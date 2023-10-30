import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MedicineService } from 'src/app/core/services/medicine.service';
import { Medicine } from 'src/app/models/medicine';

@Component({
  selector: 'app-medcine-modal',
  templateUrl: './medcine-modal.component.html',
  styleUrls: ['./medcine-modal.component.scss']
})
export class MedcineModalComponent implements OnInit {
  @Input() visible: boolean = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() medicineCreated = new EventEmitter<Medicine>();
  createUpdateMedicineForm!: FormGroup;
  constructor(private fb: FormBuilder, private medicineService: MedicineService) {
  }
  ngOnInit(): void {
    this.intializeForm();
  }

  createUpdateMedicine() {
    if (this.createUpdateMedicineForm.get('id')?.value == 0) {
      this.createMedicine();
    } else {
      this.updateMedicine();
    }
    this.modelToggeled(false);
  }

  updateMedicine() {
    this.medicineService.updateMedicine(this.createUpdateMedicineForm.value).subscribe(response => {
      this.medicineCreated.emit(response);
    })
  }

  createMedicine() {
    this.medicineService.createMedicine(this.createUpdateMedicineForm.value).subscribe(response => {
      this.medicineCreated.emit(response);
    })
  }

  modelToggeled(e: any) {
    this.visible = e;
    this.visibleChange.emit(this.visible);
  }

  intializeForm() {
    this.createUpdateMedicineForm = this.fb.group({
      id: [0, Validators.required],
      name: ['', Validators.required],
    });
  }
}
