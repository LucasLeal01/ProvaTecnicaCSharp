import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

export interface Notification {
  id: number;
  message: string;
  type: 'success' | 'error' | 'warning' | 'info';
  duration?: number;
  'fade-out'?: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private notifications$ = new Subject<Notification>();
  private currentId = 0;

  getNotifications() {
    return this.notifications$.asObservable();
  }

  showSuccess(message: string, duration: number = 5000) {
    this.showNotification(message, 'success', duration);
  }

  showError(message: string, duration: number = 5000) {
    this.showNotification(message, 'error', duration);
  }

  showWarning(message: string, duration: number = 5000) {
    this.showNotification(message, 'warning', duration);
  }

  showInfo(message: string, duration: number = 5000) {
    this.showNotification(message, 'info', duration);
  }

  private showNotification(message: string, type: 'success' | 'error' | 'warning' | 'info', duration: number) {
    const notification: Notification = {
      id: ++this.currentId,
      message,
      type,
      duration
    };
    this.notifications$.next(notification);
  }
} 