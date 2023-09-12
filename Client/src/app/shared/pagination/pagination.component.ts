import { Component, Input, Output, EventEmitter, forwardRef, OnInit } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PaginationComponent),
      multi: true
    }
  ]
})
export class PaginationComponent implements ControlValueAccessor, OnInit {
  @Input() totalItems: number = 0;
  @Input() itemsPerPage: number = 0;
  @Input() boundaryLinks: boolean = false;
  @Output() pageChanged: EventEmitter<number> = new EventEmitter<number>();
  
  currentPage: number = 0;
  totalPages: number = 0;
  ngOnInit(): void {
    this.totalPages = Math.ceil(this.totalItems / this.itemsPerPage);
  }
  writeValue(value: number): void {
    this.currentPage = value;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    // Optional: Implement if you need to disable the component
  }

  // Define a method to update the currentPage property
  updatePage(value: number) {
    this.currentPage = value;
    this.pageChanged.emit(value);
    this.onChange(value);
    this.onTouched();
  }

  private onChange: any = () => {};
  private onTouched: any = () => {};

  goToFirstPage() {
    this.updatePage(1);
  }

  onPageChange(page: number) {
    this.updatePage(page);
  }

  onPrevNextChange(isNext: boolean) {
    const newPage = isNext ? this.currentPage + 1 : this.currentPage - 1;
    this.updatePage(newPage);
  }

  goToLastPage() {
    this.updatePage(this.totalPages);
  }
}
