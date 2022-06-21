import {
    AfterViewInit,
    Component,
    ElementRef,
    forwardRef,
    Injector,
    Input,
    OnDestroy,
    OnInit,
    Provider,
    ViewChild,
    ViewEncapsulation
} from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { BooleanInput, coerceBooleanProperty } from '@angular/cdk/coercion';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { fromEvent, Subject } from 'rxjs';
// import {AppComponentBase} from '@shared/common/app-component-base';
// import {AngularEditorComponent} from '@kolkov/angular-editor';
// import {AngularEditorConfig} from '@node_modules/@kolkov/angular-editor/lib/config';
// import {fromEvent, Subject} from '@node_modules/rxjs';

declare const ClassicEditor: any;

const VALUE_ACCESSOR: Provider = {
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => InputCkeditorComponent),
    multi: true
};

@Component({
    selector: 'app-input-ckeditor',
    template: `
      <div [hidden]="readonly">
          <div #refElemtEditor style="min-width:800px"></div>
      </div>
      <div [hidden]="!readonly" class="ck ck-reset ck-editor ck-rounded-corners overflow-inherit" 
      role="application" dir="ltr" lang="vi" aria-labelledby="ck-editor__label_e27aedac41ca885457c175e3fb3424c23" >
          <div class="ck ck-editor__main" role="presentation">
              <div
                  class="content-ora ck-blurred ck ck-content ck-editor__editable ck-rounded-corners ck-editor__editable_inline ck-read-only"
                  lang="vi" dir="ltr" role="textbox" aria-label="Trình soạn thảo văn bản, main" contenteditable="false">
                  <div [innerHTML]="value | safe:'html'"></div>
              </div>
          </div>
      </div>
  `,
    styleUrls: ['./input-ckeditor.component.scss'],
    providers: [VALUE_ACCESSOR],
})

export class InputCkeditorComponent implements OnInit, AfterViewInit, ControlValueAccessor, OnDestroy {
    @ViewChild('refElemtEditor', { static: true }) refElemtEditor: ElementRef;
    _value = '';
    editor: any;
    $destroy: Subject<boolean> = new Subject<boolean>();
    $setValue: Subject<any> = new Subject<any>();
    listItem = [];

    @Input()
    get value() {
        return this._value;
    }
    set value(v: any) {
        this._value = v;
    }
    @Input() itemSimple = false;
    @Input() readonly = false;
    @Input() disabled = false;


    constructor() { }

    private onChange: Function = (v: any) => {
    };
    private onTouched: Function = () => {
    };

    ngOnInit() {
        if (this.itemSimple) {
            this.listItem = [
                'heading',
                '|',
                'bold',
                'italic',
                'underline',
                'link',
                'bulletedList',
                'numberedList',
                '|',
                'horizontalLine',
                'alignment',
                'outdent',
                'indent',
                '|',
                'imageUpload',
                'blockQuote',
                'insertTable',
                'mediaEmbed',
                'undo',
                'redo',
            ];
        } else {
            this.listItem = [
                'heading',
                '|',
                'fontFamily',
                'fontSize',
                'fontColor',
                'fontBackgroundColor',
                'highlight',
                '|',
                'bold',
                'italic',
                'underline',
                'strikethrough',
                'link',
                'bulletedList',
                'numberedList',
                'todoList',
                '|',
                'horizontalLine',
                'alignment',
                'outdent',
                'indent',
                'pageBreak',
                '|',
                'imageUpload',
                'blockQuote',
                'insertTable',
                'mediaEmbed',
                'undo',
                'redo',
                '|',
                'superscript',
                'subscript',
                'specialCharacters'
            ];
        }
        this.$setValue.pipe(takeUntil(this.$destroy), debounceTime(100), distinctUntilChanged())
            .subscribe(result => {
                this.editor.setData(result ? result : '');
            }
        );
    }

    ngAfterViewInit(): void {
        setTimeout(() => {
            this.initEditor();
        }, 100);
    }

    ngOnDestroy(): void {
        this.$destroy.next(true);
        this.$destroy.unsubscribe();
        if (this.editor) {
            this.editor.destroy().catch(error => { });
        }
    }

    initEditor() {
        ClassicEditor
            .create(this.refElemtEditor.nativeElement, {
                language: 'vi',
                toolbar: {
                    items: this.listItem
                },
                image: {
                    toolbar: [
                        'imageTextAlternative',
                        'imageStyle:inline',
                        'imageStyle:block',
                        'imageStyle:side',
                        'linkImage'
                    ]
                },
                table: {
                    contentToolbar: [
                        'tableColumn',
                        'tableRow',
                        'mergeTableCells',
                        'tableCellProperties',
                        'tableProperties'
                    ]
                },
                link: {
                    decorators: {
                        openInNewTab: {
                            mode: 'manual',
                            label: 'Open in a new tab',
                            attributes: {
                                target: '_blank',
                                rel: 'noopener noreferrer'
                            }
                        }
                    }
                },
                licenseKey: '',
            })
            .then(editor => {
                this.editor = editor;

                if (this.readonly) {
                    editor.enableReadOnlyMode(this.editor.id);
                } else {
                    editor.disableReadOnlyMode(this.editor.id);
                }

                this.editor.setData(this.value ? this.value : '');
                fromEvent(this.editor.model.document, 'change').pipe(
                    debounceTime(300)
                ).subscribe(() => {
                    this.onChangeValue(this.editor.getData());
                });
            })
            .catch(error => {
                console.error('Oops, something gone wrong!');
                console.error('Please, report the following error in the https://github.com/ckeditor/ckeditor5 with the build id and the error stack trace:');
                console.warn('Build id: imc1gqj0halx-6j4jigac1f1v');
                console.error(error);
            });
    }

    onChangeValue(event: any): void {
        this.value = event;
        this.onChange(event);
    }

    onFocus(event: any): void {
        this.onTouched();
    }


    writeValue(obj: any): void {
        this._value = obj;
        console.log('writeValue');
        if (this.editor && obj) {
            this.$setValue.next(obj);
        }
    }

    registerOnChange(fn: Function): void {
        this.onChange = fn;
    }

    registerOnTouched(fn: Function): void {
        this.onTouched = fn;
    }

    setDisabledState?(isDisabled: boolean): void {
        this.disabled = isDisabled;
    }

}

