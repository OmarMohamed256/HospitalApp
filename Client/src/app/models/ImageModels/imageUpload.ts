export interface ImageUpload {
    file:      File;
    userId:    number;
    category:  string;
    imageDate: Date;
    type?:     string;
    organ?:    string;
}