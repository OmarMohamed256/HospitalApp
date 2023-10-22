export interface Image {
    id:        number;
    url:       string;
    userId:    number;
    category:  string;
    imageDate: Date;
    dateCreated: Date;
    type?:     string;
    organ?:    string;
}