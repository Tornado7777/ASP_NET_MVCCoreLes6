﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateEngine.Docx;

namespace lesson6.Service.Impl
{
    internal class ProductReportWord : IProductReport
    {
        #region Private Fields

        private const string _FieldCatalogName = "CatalogName";
        private const string _FieldCatalogDescription = "CatalogDescription";
        private const string _FieldCreateionDate = "CreateionDate";

        private readonly FileInfo _templateFile;

        #endregion

        #region Public Properties
        public string CatalogName { get; set; } = null!;
        public string CatalogDescription { get; set; } = null!;
        public DateTime CreateionDate { get; set; }
        public IEnumerable<(int id, string name, string category, decimal price)> Products { get; set; }

        #endregion


        #region Contrucror
        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateFile">Наименование фала-шаблона</param>
        public ProductReportWord(string templateFile)
        {
            
            _templateFile = new FileInfo(templateFile);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportFilePath">Наименование фала-отчета</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public FileInfo Create(string reportFilePath)
        {
            if (!_templateFile.Exists)
                throw new FileNotFoundException();
            var reportFile = new FileInfo(reportFilePath);
            reportFile.Delete();
            _templateFile.CopyTo(reportFile.FullName);

            var conent = new Content(
                new FieldContent(_FieldCatalogName, CatalogName),
                new FieldContent(_FieldCatalogDescription, CatalogDescription),
                new FieldContent(_FieldCreateionDate, CreateionDate.ToString("dd.MM.yyyy HH:mm:ss"))
                );

            var templateProcessor = new TemplateProcessor(reportFile.FullName)
                .SetRemoveContentControls(true);

            templateProcessor.FillContent(conent);
            templateProcessor.SaveChanges();
            reportFile.Refresh();

            return reportFile;
        }
    }
}