export class TimeSpan {
    constructor(public ticks: number) {}
  
    get days(): number {
      return Math.floor(this.ticks / (1000 * 60 * 60 * 24));
    }
  
    get hours(): number {
      return Math.floor((this.ticks % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    }
  
    get minutes(): number {
      return Math.floor((this.ticks % (1000 * 60 * 60)) / (1000 * 60));
    }
  
    get seconds(): number {
      return Math.floor((this.ticks % (1000 * 60)) / 1000);
    }
  
    get milliseconds(): number {
      return this.ticks % 1000;
    }
  
    add(other: TimeSpan): TimeSpan {
      return new TimeSpan(this.ticks + other.ticks);
    }
  
    subtract(other: TimeSpan): TimeSpan {
      return new TimeSpan(this.ticks - other.ticks);
    }
  
    compareTo(other: TimeSpan): number {
      if (this.ticks > other.ticks) {
        return 1;
      } else if (this.ticks < other.ticks) {
        return -1;
      } else {
        return 0;
      }
    }
  
    equals(other: TimeSpan): boolean {
      return this.ticks === other.ticks;
    }
  
    toString(): string {
        return `${this.hours.toString().padStart(2, '0')}:${this.minutes.toString().padStart(2, '0')}:${this.seconds.toString().padStart(2, '0')}.${this.milliseconds.toString().padStart(7, '0')}`;
      }
  }