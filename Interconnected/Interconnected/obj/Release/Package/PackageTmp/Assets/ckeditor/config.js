/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	 //config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.filebrowserBrowseUrl = '/Assets/fileman/index.html?integration=ckeditor',
    config.filebrowserImageBrowseUrl = '/Assets/fileman/index.html?integration=ckeditors&type=image',
    config.removeDialogTabs = 'link:upload;image:upload'
    //config.filebrowserFlashBrowseUrl = '/Assets/ckfinder/ckfinder.html?type=Flash',
    //config.filebrowserUploadUrl = '/Assets/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files',
    //config.filebrowserImageUploadUrl = '/Assets/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images',
    //config.filebrowserFlashUploadUrl = '/Assets/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash'
};
